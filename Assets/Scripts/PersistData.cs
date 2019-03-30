using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class PersistData : MonoBehaviour {
	
	public static PersistData instance;

	public GameObject go_Window;
	public Text t_Window;
	public Button b_Load;

	void Awake () {
		
		if (instance == null)
			instance = this;

		if (File.Exists (Application.persistentDataPath + "/PlayerData.DAT")) 
			b_Load.interactable = true;
		else
			b_Load.interactable = false;
	}
	
	public void SaveGame () {
		
		BinaryFormatter bf_Writer = new BinaryFormatter ();
		FileStream fs_File = File.Create (Application.persistentDataPath + "/PlayerData.DAT");

		KnigthsData p_Data = new KnigthsData ();

		int i_KnightIndex = 0;

		for (int row = 0; row < KnightMatrix.instance._kKnights.GetLength(0); row++)
		{
			for (int col = 0; col < KnightMatrix.instance._kKnights.GetLength(1); col++)
			{
				if (KnightMatrix.instance._kKnights[row, col].i_SquareContent == -1) continue;

				p_Data.b_KnightsVis [i_KnightIndex] = KnightMatrix.instance._kKnights [row, col].b_IsVisible;
				p_Data.i_KnightsSeq [i_KnightIndex] = KnightMatrix.instance._kKnights [row, col].i_Sequence;
				p_Data.i_KnightsCon [i_KnightIndex] = KnightMatrix.instance._kKnights [row, col].i_SquareContent;
				i_KnightIndex++;
			}
		}

		p_Data.f_TotalTime = Score.instance.GetTime ();
		p_Data.i_CurrentMoves = Score.instance.GetScore ();

		bf_Writer.Serialize (fs_File, p_Data);

		go_Window.SetActive (true);
		t_Window.text = "Successfully Saved";
		b_Load.interactable = true;
	}
	
	public void LoadGame () {
		
		if (File.Exists(Application.persistentDataPath + "/PlayerData.DAT")){
			
			BinaryFormatter bf_Reader = new BinaryFormatter();
			FileStream fs_File = File.Open (Application.persistentDataPath + "/PlayerData.DAT", FileMode.Open);
			
			KnigthsData p_Data = (KnigthsData)bf_Reader.Deserialize(fs_File);

			int i_KnightIndex = 0;

			for (int row = 0; row < KnightMatrix.instance._kKnights.GetLength(0); row++)
			{
				for (int col = 0; col < KnightMatrix.instance._kKnights.GetLength(1); col++)
				{
					if (KnightMatrix.instance._kKnights[row, col].i_SquareContent == -1) continue;

					KnightMatrix.instance._kKnights [row, col].b_IsVisible = p_Data.b_KnightsVis [i_KnightIndex];
					KnightMatrix.instance._kKnights [row, col].i_SquareContent = p_Data.i_KnightsCon [i_KnightIndex];
					KnightMatrix.instance._kKnights [row, col].i_Sequence = p_Data.i_KnightsSeq [i_KnightIndex];

					if (KnightMatrix.instance._kKnights [row, col].i_SquareContent == 0)
						KnightMatrix.instance._kKnights [row, col].Deactivate ();
					else
						KnightMatrix.instance._kKnights [row, col].Activate ();
					
					KnightMatrix.instance._kKnights [row, col].LoadKnight ();

					i_KnightIndex++;
				}
			}

			Score.instance.SetTime (p_Data.f_TotalTime);
			Score.instance.SetScore (p_Data.i_CurrentMoves);

			fs_File.Close();

			go_Window.SetActive (true);
			t_Window.text = "Successfully Loaded";
		}
	}

}


[Serializable]
class KnigthsData {
	
	public bool[] b_KnightsVis = new bool[25];
	public int[] i_KnightsSeq = new int[25];
	public int[] i_KnightsCon = new int[25];

	public float f_TotalTime;
	public int i_CurrentMoves;
}