package md555459b1ea8ea4b48d84a723e4539e0a9;


public class BellScheduleActivity
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
		mono.android.Runtime.register ("MySpringbrook.Droid.Resources.scripts.BellScheduleActivity, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BellScheduleActivity.class, __md_methods);
	}


	public BellScheduleActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BellScheduleActivity.class)
			mono.android.TypeManager.Activate ("MySpringbrook.Droid.Resources.scripts.BellScheduleActivity, MySpringbrook.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
