using UnityEngine;
using System;
using System.Collections.Generic;


public class Theme{
	private string id;
	private string name;
	private string image;
	static int themeAttribute = 3;

	public void setId(string id){
		this.id = id;
	}

	public string getId(){
		return this.id;
	}

	public void setName(string name){
		this.name = name;
	}

	public string getName(){
		return this.name;
	}

	public void setImage(string image){
		this.image = image;
	}

	public string getImage(){
		return this.image;
	}

	static public Dictionary<String,Theme> genThemeList(string[] obj){
		int num = themeAttribute;
		//int i = 1;
		Dictionary<String,Theme> themeList = new Dictionary<String,Theme> ();
		Theme t = null;

		foreach(string o in obj){
			if (num == 3) {
				t = new Theme ();
			}
			if(o.Contains("id")){
				
			}
		}

		return themeList;
	}
}
