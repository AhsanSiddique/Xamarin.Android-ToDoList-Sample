using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using System;
using ToDoList.Helper;
using System.Collections.Generic;

namespace ToDoList
{
    [Activity(Label = "ToDoList", MainLauncher = true , Theme ="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        EditText edtTask;
        DbHelper dbHelper;
        CustomAdapter adapter;
        ListView lstTask;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_item, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    edtTask = new EditText(this);
                    Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Task")
                        .SetMessage("What do you want to do next?")
                        .SetView(edtTask)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    alertDialog.Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = edtTask.Text;
            dbHelper.InsertNewTask(task);
            LoadTaskList();
        }

        public void LoadTaskList()
        {
            List<string> taskList = dbHelper.getTaskList();
            adapter = new CustomAdapter(this, taskList, dbHelper);
            lstTask.Adapter = adapter;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            dbHelper = new DbHelper(this);
            lstTask = FindViewById<ListView>(Resource.Id.lstTask);

            //Load Data

            LoadTaskList();
        }
    }
}

