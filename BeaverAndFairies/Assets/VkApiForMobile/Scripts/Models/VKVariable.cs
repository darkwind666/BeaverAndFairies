using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VKVariable {
	public static VKVariable Deserialize(object variable)
	{
		var _variable=new VKVariable();
		var data=(Dictionary<string,object>)variable;
		object key,value;
		if(data.TryGetValue("key",out key))
		   _variable.key=(string)key;
		if(data.TryGetValue("value",out value))
			_variable.value=(string)value;
		return _variable;
	}
	public string key{get;set;}
	public string value{get;set;}

}
