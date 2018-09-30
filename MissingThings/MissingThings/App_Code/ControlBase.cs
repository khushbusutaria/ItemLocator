using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ControlBase
/// </summary>
public class ControlBase : System.Web.UI.UserControl
{
    public string strServerURL = PageBase.GetServerURL() + "/";
	public ControlBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}