package md5eb283b94bf7e5ca0fd5f26d9136caf03;


public class AddTaskDialog
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MySpringbrook.Droid.AddTaskDialog, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AddTaskDialog.class, __md_methods);
	}


	public AddTaskDialog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AddTaskDialog.class)
			mono.android.TypeManager.Activate ("MySpringbrook.Droid.AddTaskDialog, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
