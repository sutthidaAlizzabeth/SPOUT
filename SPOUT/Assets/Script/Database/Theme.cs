using UnityEngine;
using System;
using System.Collections.Generic;


public class Theme{
	public string id;
	public string name;
	public string image;

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


}
