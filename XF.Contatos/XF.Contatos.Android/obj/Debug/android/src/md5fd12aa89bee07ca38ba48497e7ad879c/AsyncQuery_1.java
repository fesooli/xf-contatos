package md5fd12aa89bee07ca38ba48497e7ad879c;


public class AsyncQuery_1
	extends android.content.AsyncQueryHandler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onQueryComplete:(ILjava/lang/Object;Landroid/database/Cursor;)V:GetOnQueryComplete_ILjava_lang_Object_Landroid_database_Cursor_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.AsyncQuery`1, Xamarin.Mobile", AsyncQuery_1.class, __md_methods);
	}


	public AsyncQuery_1 (android.content.ContentResolver p0)
	{
		super (p0);
		if (getClass () == AsyncQuery_1.class)
			mono.android.TypeManager.Activate ("Xamarin.AsyncQuery`1, Xamarin.Mobile", "Android.Content.ContentResolver, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onQueryComplete (int p0, java.lang.Object p1, android.database.Cursor p2)
	{
		n_onQueryComplete (p0, p1, p2);
	}

	private native void n_onQueryComplete (int p0, java.lang.Object p1, android.database.Cursor p2);

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
