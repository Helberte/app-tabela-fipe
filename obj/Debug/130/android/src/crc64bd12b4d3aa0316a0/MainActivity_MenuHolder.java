package crc64bd12b4d3aa0316a0;


public class MainActivity_MenuHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Tabela_FIPE.MainActivity+MenuHolder, Tabela-FIPE", MainActivity_MenuHolder.class, __md_methods);
	}


	public MainActivity_MenuHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == MainActivity_MenuHolder.class) {
			mono.android.TypeManager.Activate ("Tabela_FIPE.MainActivity+MenuHolder, Tabela-FIPE", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
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
