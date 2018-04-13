using UnityEngine;
using System.Collections;
using System.Collections.Generic;　　　　// 追記

public class CSVLoader : MonoBehaviour {

	[HeaderAttribute("読み込むCSVファイル")] public TextAsset csvFile; // Inspectorでcsvファイルを割当
	[HeaderAttribute("生成位置（LeftTop）")] public Vector2 createMapStartPosition; //Mapを生成する初期位置
	[HeaderAttribute("生成するマップチップ（csvNumverはCSVファイルの数字に対応）")] public CreateMapData[] createMapData;

	private List<string[]> csvData = new List<string[]>(); //読み取ったCSVの情報を格納する

	private Vector2 createMapPosition;
	private int i, j,k;
	private int lineCount; //行数　ループに使う
	private int itemCount; //項目数　ループに使う
	private int dataCount; //マップチップのデータ数　ループに使う

	private void Awake () {

		// 格納
		string[] lines = csvFile.text.Replace("\r\n", "\n").Split("\n"[0]);
		foreach (var line in lines){
			if (line == "") {continue;}
			csvData.Add(line.Split(','));　　　　// string[]を追加している
		}
		// 書き出し
		/*	Debug.Log (csvDatas.Count);　　　　　　　　  // 行数
			Debug.Log (csvDatas[0].Length);　　　　　　  // 項目数
			Debug.Log (csvDatas [1] [2]);*/            // 2行目3列目

		lineCount = csvData.Count;
		itemCount = csvData[0].Length;
		dataCount = createMapData.Length;

		MapCreate ();　　　　
	}
	
	void MapCreate()
	{
		for(i = 0; i < lineCount; i++)
		{
			for(j = 0; j < itemCount; j++)
			{
				for(k = 0; k < dataCount; k++)
				{
					if(csvData[i][j] == createMapData[k].csvNumber)
					{
						createMapPosition.x = j + createMapStartPosition.x;
						createMapPosition.y = -i + createMapStartPosition.y;

						GameObject map = Instantiate(createMapData[k].mapPrefab,
					createMapPosition, transform.rotation) as GameObject;
						map.transform.parent = transform;
					}
				}
			}
		}
	}
}

[System.Serializable]
public class CreateMapData
{
	public GameObject mapPrefab;
	public string csvNumber;
}