using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace ToDoList.Helper
{
    public class CustomAdapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<string> taskList;
        private DbHelper dbHelper;

        public CustomAdapter(MainActivity mainActivity, List<string> taskList, DbHelper dbHelper)
        {
            this.mainActivity = mainActivity;
            this.taskList = taskList;
            this.dbHelper = dbHelper;
        }

        public override int Count { get { return taskList.Count; } }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row, null);
            TextView txtTask = view.FindViewById<TextView>(Resource.Id.task_title);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);
            txtTask.Text = taskList[position];
            btnDelete.Click += delegate 
            {
                string task = taskList[position];
                dbHelper.deleteTask(task);
                mainActivity.LoadTaskList(); // Reload Data
            };
            return view;
        }
    }
}