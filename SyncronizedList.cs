using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace PT_Sguil
{
    public class SyncList<T> : BindingList<T>
    {
        private Action<ListChangedEventArgs> _FireEventAction;
        private ISynchronizeInvoke _SyncObject;
        private bool isSorted;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;


        public SyncList(ISynchronizeInvoke syncObject)
        {
            this.isSorted = false;
            this.sortDirection = ListSortDirection.Ascending;
            this.sortProperty = null;
            this._SyncObject = syncObject;
            this._FireEventAction = new Action<ListChangedEventArgs>(base.OnListChanged);
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {

            for (int i = 0; i < Count; ++i)
            {
                if (key.Equals(prop.GetValue(Items[i])))
                    return i;
            }

            return -1;

        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            this.sortProperty = prop;
            this.sortDirection = direction;
            List<T> items = base.Items as List<T>;

            try
            {
                if (items != null)
                {
                    PropertyComparer<T> comparer = new PropertyComparer<T>(prop, direction);
                    items.Sort(comparer);
                    this.isSorted = true;
                }
                else
                {
                    this.isSorted = false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            
        }

        /**
        private void FireEvent(ListChangedEventArgs args)
        {
            base.OnListChanged(args);
        }
       **/

        protected override void OnListChanged(ListChangedEventArgs args)
        {
            lock (this)
            {
                if (_SyncObject != null && _SyncObject.InvokeRequired)
                {
                    _SyncObject.Invoke(this._FireEventAction, new object[] { args });
                }
                else base.OnListChanged(args);
            }
        }

        protected override bool IsSortedCore
        {
            get
            {
                return this.isSorted;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return this.sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return this.sortProperty;
            }
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        public int Find(string property, object key)
        {
            // Check the properties for a property with the specified name.
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            else
                return FindCore(prop, key);
        }
    }

    public class PropertyComparer<T> : IComparer<T>
    {
        private PropertyDescriptor _property;
        private ListSortDirection _sortDirection;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this._property = property;
            this._sortDirection = direction;
        }

        public int Compare(T x, T y)
        {
            int num = 0;
            object a = this._property.GetValue(x);
            object b = this._property.GetValue(y);
            num = Comparer.Default.Compare(a, b);
            if (this._sortDirection == ListSortDirection.Descending)
            {
                num = -num;
            }
            return num;
        }
    }
}

