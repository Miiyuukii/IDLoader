using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unstblr.IDLoader;
using Unstblr.Common;

public class IDLoaderEditor : MonoBehaviour
{
    [MenuItem("GameObject/Unstblr/Add to scene/IDLoader")]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("IDLoader");
        go.AddComponent<IDManager>();
        GameObject child = new GameObject("StringLoader");
        child.AddComponent<StringLoader>();
        child.transform.parent = go.transform;
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

}