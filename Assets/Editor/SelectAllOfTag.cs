using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectAllOfTag : ScriptableWizard
{
	const string WizardName = "Select All Of Tag...";
	const string CreateButtonName = "Make Selection";
	public string searchTag = string.Empty;

	[MenuItem ("My Tools/" + WizardName)]
	static void SelectAllOfTagWizard()
	{
		ScriptableWizard.DisplayWizard<SelectAllOfTag>(WizardName, CreateButtonName);
	}

	void OnWizardCreate()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(searchTag);
		Selection.objects = gameObjects;	//Select all found gameobjects on hierarchy
	}

}
