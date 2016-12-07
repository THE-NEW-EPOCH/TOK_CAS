package md5eb283b94bf7e5ca0fd5f26d9136caf03;


public class AddTaskDone
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MySpringbrook.Droid.AddTaskDone, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AddTaskDone.class, __md_methods);
	}


	public AddTaskDone () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AddTaskDone.class)
			mono.android.TypeManager.Activate ("MySpringbrook.Droid.AddTaskDone, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
