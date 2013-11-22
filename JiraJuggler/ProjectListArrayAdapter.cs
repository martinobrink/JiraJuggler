using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JiraJuggler.Shared.Model;

namespace JiraJuggler
{
    public class ProjectListArrayAdapter : ArrayAdapter<ProjectData>
    {
        private readonly Context _context;

        public ProjectListArrayAdapter(Context context) : base(context, Resource.Layout.ProjectListItem)
        {
            _context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var projectData = GetItem(position);
            var textView = new TextView(_context);
            textView.Text = projectData.Name;
            return textView;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var projectData = GetItem(position);
            var textView = new TextView(_context);
            textView.Text = projectData.Name;
            return textView;
        }


        public void AddAll(IEnumerable<ProjectData> projects)
        {
            foreach (var project in projects)
            {
                Add(project);
            }
        }


        public ProjectListArrayAdapter(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public ProjectListArrayAdapter(Context context, int textViewResourceId) : base(context, textViewResourceId)
        {
        }

        public ProjectListArrayAdapter(Context context, int resource, int textViewResourceId) : base(context, resource, textViewResourceId)
        {
        }

        public ProjectListArrayAdapter(Context context, int textViewResourceId, ProjectData[] objects) : base(context, textViewResourceId, objects)
        {
        }

        public ProjectListArrayAdapter(Context context, int resource, int textViewResourceId, ProjectData[] objects) : base(context, resource, textViewResourceId, objects)
        {
        }

        public ProjectListArrayAdapter(Context context, int textViewResourceId, IList<ProjectData> objects) : base(context, textViewResourceId, objects)
        {
        }

        public ProjectListArrayAdapter(Context context, int resource, int textViewResourceId, IList<ProjectData> objects) : base(context, resource, textViewResourceId, objects)
        {
        }
    }
}