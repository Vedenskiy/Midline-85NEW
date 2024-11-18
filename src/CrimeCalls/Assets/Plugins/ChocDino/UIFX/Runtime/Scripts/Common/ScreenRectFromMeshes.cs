//--------------------------------------------------------------------------//
// Copyright 2023-2024 Chocolate Dinosaur Ltd. All rights reserved.         //
// For full documentation visit https://www.chocolatedinosaur.com           //
//--------------------------------------------------------------------------//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChocDino.UIFX
{
	public struct RectAdjustOptions
	{
		public int padding;
		public int roundToNextMultiple;
		public bool clampToScreen;
	}

	/// <summary>
	/// Takes a collection of Mesh / VertexHelper and calculates the screen-space rectangle that would encapsulate all of them.
	/// </summary>
	public class ScreenRectFromMeshes
	{
		private bool _isFirstPoint;
		internal Bounds _screenBounds;
		private Rect _screenRect;
		private Rect _localRect;
		private RectInt _textureRect;
		private Camera _camera;

		private static Vector3[] s_boundsPoints = new Vector3[8];
		private static Plane[] s_planes = new Plane[6];

		public void Start(Camera camera)
		{
			_screenBounds = new Bounds();
			_screenRect = Rect.zero;
			_localRect = Rect.zero;
			_camera = camera;
			_isFirstPoint = true;
		}

		public void AddMeshBounds(Transform xform, Mesh mesh)
		{
			if (mesh)
			{
				if (mesh.vertexCount > 0)
				{
					AddBounds(xform, mesh.bounds.min, mesh.bounds.max);
				}
			}
		}

		public void AddVertexBounds(Transform xform, Vector3[] verts, int vertexCount)
		{
			if (verts != null && vertexCount > 0)
			{
				Vector3 boundsMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 boundsMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

				for (int i = 0; i < vertexCount; i++)
				{
					boundsMin = Vector3.Min(boundsMin, verts[i]);
					boundsMax = Vector3.Max(boundsMax, verts[i]);
				}

				AddBounds(xform, boundsMin, boundsMax);
			}
		}

		public void AddVertexBounds(Transform xform, VertexHelper verts)
		{
			if (verts != null)
			{
				// TODO: is there a more efficient way to get the bounds of VertexHelper?
				int vertexCount = verts.currentVertCount;
				if (vertexCount > 0)
				{
					List<UIVertex> v = new List<UIVertex>(vertexCount);
					verts.GetUIVertexStream(v);
				
					Vector3 boundsMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
					Vector3 boundsMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

					for (int i = 0; i < vertexCount; i++)
					{
						boundsMin = Vector3.Min(boundsMin, v[i].position);
						boundsMax = Vector3.Max(boundsMax, v[i].position);
					}

					AddBounds(xform, boundsMin, boundsMax);
				}
			}
		}

		private void AddBounds(Transform xform, Vector3 boundsMin, Vector3 boundsMax)
		{
			_localRect = Rect.MinMaxRect(boundsMin.x, boundsMin.y, boundsMax.x, boundsMax.y);

			// Points of our bounding box
			s_boundsPoints[0] = boundsMin;
			s_boundsPoints[1] = boundsMax;
			s_boundsPoints[2] = new Vector3(boundsMin.x, boundsMin.y, boundsMax.z);
			s_boundsPoints[3] = new Vector3(boundsMin.x, boundsMax.y, boundsMin.z);
			s_boundsPoints[4] = new Vector3(boundsMax.x, boundsMin.y, boundsMin.z);
			s_boundsPoints[5] = new Vector3(boundsMin.x, boundsMax.y, boundsMax.z);
			s_boundsPoints[6] = new Vector3(boundsMax.x, boundsMin.y, boundsMax.z);
			s_boundsPoints[7] = new Vector3(boundsMax.x, boundsMax.y, boundsMin.z);

			// Convert the local AABB points to world space points
			if (xform)
			{
				for (int i = 0; i < s_boundsPoints.Length; i++)
				{
					s_boundsPoints[i] = xform.localToWorldMatrix.MultiplyPoint(s_boundsPoints[i]);
				}
			}

			// First check if any or all of the points are behind frustum planes
			bool needsClipping = false;
			if (_camera)
			{
				GeometryUtility.CalculateFrustumPlanes(_camera, s_planes);
				Debug.Assert(s_planes.Length == 6);

				int behindCount = 0;
				for (int i = 0; i < s_boundsPoints.Length; i++)
				{
					for (int j = 0; j < 6; j++)
					{
						bool isBehindPlane = !s_planes[j].GetSide(s_boundsPoints[i]); 
						if (isBehindPlane)
						{
							behindCount++;
							break;
						}
					}
				}
				needsClipping = behindCount > 0;
				if (behindCount == 8)
				{
					// All points are behind, the object is not visible, so out bounds is zero
					return;
				}
			}

			if (needsClipping)
			{
				// Convert 8 points to 12 lines so we can clip with the camera frustum
				// front: 3,7 0,4, 3,0, 7,4
				// back : 5,1, 2,6, 5,2 1,6
				// sides: 5,3 2,0 1,7 6,4
				int[] linePairs = new int[] { 3, 7, 0, 4, 3, 0, 7, 4, 5, 1, 2, 6, 5, 2, 1, 6, 5, 3, 2, 0, 1, 7, 6, 4 };
				Debug.Assert(linePairs.Length == 24);

				for (int i = 0; i < 12; i++)
				{
					Vector3 a = s_boundsPoints[linePairs[i*2+0]];
					Vector3 b = s_boundsPoints[linePairs[i*2+1]];

					// Clip A against frustum
					for (int j = 0; j < 6; j++)
					{
						Vector3 dir = (b-a).normalized;
						float distance = 0f;
						if (s_planes[j].Raycast(new Ray(a, dir), out distance))
						{
							if (distance < (a-b).magnitude)
							{
								a += dir * distance;
							}
						}
					}
					// Clip B against frustum
					for (int j = 0; j < 6; j++)
					{
						Vector3 dir = (a-b).normalized;
						float distance = 0f;
						if (s_planes[j].Raycast(new Ray(b, dir), out distance))
						{
							if (distance < (a-b).magnitude)
							{
								b += dir * distance;
							}
						}
					}

					// Convert to screen space
					a = _camera.WorldToScreenPoint(a);
					b = _camera.WorldToScreenPoint(b);

					// Add points to screen space bounding sphere
					if (!_isFirstPoint)
					{
						// Grow with the other points
						_screenBounds.Encapsulate(a);
						_screenBounds.Encapsulate(b);
					}
					else
					{
						// Initialise with the first point
						_screenBounds.center = a;
						_screenBounds.Encapsulate(b);
						_isFirstPoint = false;
					}
				}
			}
			else
			{
				for (int i = 0; i < s_boundsPoints.Length; i++)
				{
					// Convert to screen space
					if (_camera)
					{
						s_boundsPoints[i] = _camera.WorldToScreenPoint(s_boundsPoints[i]);
					}

					if (!_isFirstPoint)
					{
						// Grow with the other points
						_screenBounds.Encapsulate(s_boundsPoints[i]);
					}
					else
					{
						// Initialise with the first point
						_screenBounds.center = s_boundsPoints[i];
						_isFirstPoint = false;
					}
				}
			}
		}

		public void End()
		{
			Vector3 minScreen = _screenBounds.min;
			Vector3 maxScreen = _screenBounds.max;

			_screenRect = Rect.MinMaxRect(minScreen.x, minScreen.y, maxScreen.x, maxScreen.y);
		}

		public void Adjust(Vector2Int leftDown, Vector2Int rightUp)
		{
			//Debug.Assert(leftDown.x >= 0 && leftDown.y >= 0);
			//Debug.Assert(rightUp.x >= 0 && rightUp.y >= 0);
			_screenRect.min -= leftDown;
			_screenRect.max += rightUp;
		}

		internal void SetRect(Rect rect)
		{
			_screenRect = rect;
		}

		public Rect GetRect()
		{
			return _screenRect;
		}
		public Rect GetLocalRect()
		{
			return _localRect;
		}
		public RectInt GetTextureRect()
		{
			return _textureRect;
		}
		public void OptimiseRects(RectAdjustOptions options)
		{
			// Crop to screen size optimisation
			if (options.clampToScreen)
			{
				Rect rect = _screenRect;
				// Problem, since it's cropped, you can't see it in the Scene view which is annoying..
				// so we disable this in the editor, but have it enabled in builds.
				#if UNITY_EDITOR
				float padding = 16f;
				rect.xMin = Mathf.Max(-padding, rect.xMin);
				rect.yMin = Mathf.Max(-padding, rect.yMin);
				rect.xMax = Mathf.Max(rect.xMax, rect.xMin);
				rect.yMax = Mathf.Max(rect.yMax, rect.yMin);
				Vector2 screenResolution = Filters.GetMonitorResolution();
				rect.xMax = Mathf.Min(screenResolution.x + padding, rect.xMax);
				rect.yMax = Mathf.Min(screenResolution.y + padding, rect.yMax);
				#endif
				_innerRect = _screenRect = rect;
			}
			else
			{
				_innerRect = _screenRect;

				// To closest multiple
				// This fixes temporal flickering in BlurFilter when animating BlurSize using downsampling, by preventing rapidly switching between odd/even texture sizes
				// (For some reason 8 was the minimum value needed to fix flickering)s
				if (options.padding > 0 || options.roundToNextMultiple > 1)
				{
					int targetWidth = MathUtils.PadAndRoundToNextMultiple(_screenRect.width, options.padding, options.roundToNextMultiple);
					int targetHeight = MathUtils.PadAndRoundToNextMultiple(_screenRect.height, options.padding, options.roundToNextMultiple);

					float dx = (targetWidth - _screenRect.width);
					float dy = (targetHeight - _screenRect.height);
					// Make sure dx and dy are even to prevent rounding errors
					dx -= Mathf.CeilToInt(dx)%2;
					dy -= Mathf.CeilToInt(dy)%2;

					_screenRect.x -= dx / 2f;
					_screenRect.y -= dy / 2f;
					_screenRect.width = targetWidth;
					_screenRect.height = targetHeight;
				}

				// To closest pow2
				#if false
				if (false)
				{
					int targetWidth = Mathf.NextPowerOfTwo(Mathf.CeilToInt(_screenRect.width));
					int targetHeight = Mathf.NextPowerOfTwo(Mathf.CeilToInt(_screenRect.height));

					float dx = (targetWidth - _screenRect.width);
					float dy = (targetHeight - _screenRect.height);
					// Make sure dx and dy are even to prevent rounding errors
					dx -= Mathf.CeilToInt(dx)%2;
					dy -= Mathf.CeilToInt(dy)%2;

					_screenRect.x -= dx / 2f;
					_screenRect.y -= dy / 2f;
					_screenRect.width = targetWidth;
					_screenRect.height = targetHeight;
				}
				#endif
			}

			_textureRect = new RectInt(Mathf.FloorToInt(_screenRect.xMin), Mathf.FloorToInt(_screenRect.yMin), Mathf.CeilToInt(_screenRect.xMax) - Mathf.FloorToInt(_screenRect.xMin), Mathf.CeilToInt(_screenRect.yMax) - Mathf.FloorToInt(_screenRect.yMin));
		}

		private Rect _innerRect;

		public void BuildScreenQuad(Camera camera, Transform xform, float alpha, VertexHelper vh)
		{
			// 4 corners of quad
			Rect screenRect = _innerRect;
			Vector3 v0 = new Vector2(screenRect.xMin, screenRect.yMax);
			Vector3 v1 = new Vector2(screenRect.xMax, screenRect.yMax);
			Vector3 v2 = new Vector2(screenRect.xMax, screenRect.yMin);
			Vector3 v3 = new Vector2(screenRect.xMin, screenRect.yMin);

			// Set depth
			v3.z = v2.z = v1.z = v0.z = _screenBounds.max.z;

			// Convert screen to world space
			if (camera)
			{
				v0 = camera.ScreenToWorldPoint(v0);
				v1 = camera.ScreenToWorldPoint(v1);
				v2 = camera.ScreenToWorldPoint(v2);
				v3 = camera.ScreenToWorldPoint(v3);
			}

			// Convert world to local space
			if (xform)
			{
				Matrix4x4 worldToLocal = xform.worldToLocalMatrix;
				v0 = worldToLocal.MultiplyPoint(v0);
				v1 = worldToLocal.MultiplyPoint(v1);
				v2 = worldToLocal.MultiplyPoint(v2);
				v3 = worldToLocal.MultiplyPoint(v3);
			}

			// Zero Z
			//v3.z = v2.z = v1.z = v0.z = 0.0f;

			// Texture range
			Rect textureRect = new Rect(Mathf.FloorToInt(_screenRect.xMin), Mathf.FloorToInt(_screenRect.yMin), Mathf.CeilToInt(_screenRect.xMax) - Mathf.FloorToInt(_screenRect.xMin), Mathf.CeilToInt(_screenRect.yMax) - Mathf.FloorToInt(_screenRect.yMin));
			float tx1 = (_innerRect.xMin - textureRect.xMin) / textureRect.width;
			float ty1 = (_innerRect.yMin - textureRect.yMin) / textureRect.height;
			float tx2 = (_innerRect.xMax - textureRect.xMin) / textureRect.width;
			float ty2 = (_innerRect.yMax - textureRect.yMin) / textureRect.height;

			// Build geometry
			vh.Clear();
			Color32 alphaColor = new Color(1f, 1f, 1f, alpha);
			vh.AddVert(v0, alphaColor, new Vector4(tx1, ty2, 0f, 0f));
			vh.AddVert(v1, alphaColor, new Vector4(tx2, ty2, 0f, 0f));
			vh.AddVert(v2, alphaColor, new Vector4(tx2, ty1, 0f, 0f));
			vh.AddVert(v3, alphaColor, new Vector4(tx1, ty1, 0f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(0, 2, 3);
		}
	}
}