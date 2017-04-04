using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{
	const string SpritesFolderName = "sprites";

	//Called anytime a texture is imported and preprocessed
	void OnPostprocessTexture(Texture2D texture)
	{
		string lowerCaseAssetPath = assetPath.ToLower();
		bool isInSpritesDirectory = lowerCaseAssetPath.IndexOf("/"+ SpritesFolderName + "/") != -1;

		if(isInSpritesDirectory)
		{
			TextureImporter textureImporter = (TextureImporter) assetImporter;
			textureImporter.textureType = TextureImporterType.Sprite;
		}
	}
}
