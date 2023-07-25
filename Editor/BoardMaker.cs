using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace siratim.Tools
{
    public class BoardMaker : EditorWindow
    {
        // UI Elements
        private ObjectField objectField;
        private EnumField planes;
        private int rightDir;
        private IntegerField rightElement;

        // Backing Fields
        private Transform source;
        private int upDir;
        private IntegerField upElement;
        
        [MenuItem("Tools/BoardMaker")]
        private static void Init()
        {
            GetWindow<BoardMaker>("Board Maker");
        }

        private void CreateGUI()
        {
            var rVis = rootVisualElement;
            objectField = new ObjectField("Item Source")
            {
                objectType = typeof(Transform)
            };
            rVis.Add(objectField);

            rightElement = new IntegerField("Items in Right direction");
            rVis.Add(rightElement);

            upElement = new IntegerField("Items in Up direction");
            rVis.Add(upElement);

            planes = new EnumField("Chose Plane", Planes.XY);
            rVis.Add(planes);

            var spawnButton = new Button
            {
                text = "Spawn!",
                name = "btn_spawn"
            };
            spawnButton.clicked += OnClickSpawn;
            rVis.Add(spawnButton);
        }

        private void OnGUI()
        {
            source = objectField?.value as Transform;
            upDir = upElement.value;
            rightDir = rightElement.value;
        }

        private void OnClickSpawn()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (source == null) return;
            float scale;
            Vector3 rootPos;
            var plane = planes.value is Planes value ? value : Planes.XY; // null check and set default
            switch (plane)
            {
                case Planes.XY:
                    scale = source.transform.localScale.x;
                    rootPos = new Vector3(1f, 1f, 0f) * scale * 0.5f;
                    break;
                case Planes.XZ:
                    scale = source.transform.localScale.x;
                    rootPos = new Vector3(1f, 0f, 1f) * scale * 0.5f;
                    break;
                case Planes.ZY:
                    scale = source.transform.localScale.z;
                    rootPos = new Vector3(0f, 1f, 1f) * scale * 0.5f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var rootGo = new GameObject($"{rightDir}x{upDir}_{plane.ToString()}")
            {
                transform =
                {
                    localPosition = rootPos
                }
            };

            SpawnOnPlane(rootGo, plane);
        }

        private void SpawnOnPlane(GameObject root, Planes plane)
        {
            for (var r = 0; r < rightDir; r++)
            for (var u = 0; u < upDir; u++)
            {
                var a = Instantiate(source, root.transform, true);
                switch (plane)
                {
                    case Planes.XY:
                        a.localPosition = Vector3.right * r + Vector3.up * u + Vector3.forward * 0;
                        break;
                    case Planes.XZ:
                        a.localPosition = Vector3.right * r + Vector3.up * 0 + Vector3.forward * u;
                        break;
                    case Planes.ZY:
                        a.localPosition = Vector3.right * 0f + Vector3.up * u + Vector3.forward * r;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                a.name = $"{u}_{r}";
            }
        }
    }

    public enum Planes
    {
        XY = 0,
        XZ = 1,
        ZY = 2
    }
}