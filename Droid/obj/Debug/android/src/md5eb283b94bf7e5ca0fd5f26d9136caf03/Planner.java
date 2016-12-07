package md5eb283b94bf7e5ca0fd5f26d9136caf03;


public class Planner
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MySpringbrook.Droid.Planner, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Planner.class, __md_methods);
	}


	public Planner () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Planner.class)
			mono.android.TypeManager.Activate ("MySpringbrook.Droid.Planner, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
