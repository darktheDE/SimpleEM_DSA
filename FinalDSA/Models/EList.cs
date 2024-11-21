using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FinalDSA.Models
{
    public class EList<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _size;

        public EList()
        {
            // Khởi tạo với kích thước mặc định
            _items = new T[4]; 
            _size = 0;
        }

        /// <summary>
        /// Lấy số lượng phần tử hiện tại trong danh sách.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Lấy hoặc gán giá trị phần tử tại vị trí chỉ định.
        /// </summary>
        /// <param name="index">Chỉ số của phần tử trong danh sách.</param>
        /// <returns>Phần tử tại vị trí chỉ định.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException(nameof(index));
                _items[index] = value;
            }
        }

        /// <summary>
        /// Thêm một phần tử vào cuối danh sách.
        /// </summary>
        /// <param name="item">Phần tử cần thêm vào danh sách.</param>
        public void Add(T item)
        {
            if (_size == _items.Length)
                Resize();
            _items[_size++] = item;
        }

        /// <summary>
        /// Xóa phần tử tại vị trí chỉ định.
        /// </summary>
        /// <param name="index">Chỉ số của phần tử cần xóa.</param>
        /// <exception cref="ArgumentOutOfRangeException">Nếu chỉ số vượt quá phạm vi danh sách.</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = index; i < _size - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _items[--_size] = default(T);
        }

        /// <summary>
        /// Xóa tất cả các phần tử trong danh sách.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _size; i++)
            {
                _items[i] = default(T);
            }
            _size = 0;
        }

        /// <summary>
        /// Thay đổi kích thước của danh sách khi đầy.
        /// </summary>
        private void Resize()
        {
            int newSize = _items.Length * 2;
            T[] newArray = new T[newSize];
            Array.Copy(_items, newArray, _size);
            _items = newArray;
        }

        /// <summary>
        /// Sắp xếp danh sách theo một phương thức so sánh.
        /// </summary>
        /// <param name="comparison">Phương thức so sánh các phần tử.</param>
        public void Sort(Comparison<T> comparison)
        {
            Array.Sort(_items, 0, _size, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Nhóm các phần tử trong danh sách theo một khóa.
        /// </summary>
        /// <typeparam name="TKey">Loại khóa để nhóm các phần tử.</typeparam>
        /// <param name="keySelector">Hàm chọn khóa để nhóm.</param>
        /// <returns>Danh sách các nhóm phần tử.</returns>
        public IEnumerable<IGrouping<TKey, T>> GroupBy<TKey>(Func<T, TKey> keySelector)
        {
            return _items.Take(_size).GroupBy(keySelector);
        }

        /// <summary>
        /// Tính tổng giá trị của các phần tử trong danh sách theo một cách chọn.
        /// </summary>
        /// <param name="selector">Hàm chọn giá trị để tính tổng.</param>
        /// <returns>Tổng giá trị của các phần tử.</returns>
        public double Sum(Func<T, double> selector)
        {
            return _items.Take(_size).Sum(selector);
        }

        /// <summary>
        /// Sắp xếp các phần tử trong danh sách theo một khóa.
        /// </summary>
        /// <typeparam name="TKey">Loại khóa để sắp xếp.</typeparam>
        /// <param name="keySelector">Hàm chọn khóa để sắp xếp.</param>
        /// <returns>Danh sách đã được sắp xếp.</returns>
        public EList<T> OrderBy<TKey>(Func<T, TKey> keySelector)
        {
            var orderedItems = _items.Take(_size).OrderBy(keySelector).ToArray();
            EList<T> orderedList = new EList<T>();
            foreach (var item in orderedItems)
            {
                orderedList.Add(item);
            }
            return orderedList;
        }

        /// <summary>
        /// Lấy bộ lập trình các phần tử trong danh sách.
        /// </summary>
        /// <returns>Bộ lập trình các phần tử trong danh sách.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Chèn một phần tử vào danh sách tại vị trí chỉ định.
        /// </summary>
        /// <param name="index">Chỉ số vị trí cần chèn phần tử vào.</param>
        /// <param name="item">Phần tử cần chèn vào.</param>
        public void Insert(int index, T item)
        {
            _internalList.Insert(index, item);
        }

        private List<T> _internalList = new List<T>();

        /// <summary>
        /// Khởi tạo danh sách EList từ một danh sách List.
        /// </summary>
        /// <param name="list">Danh sách List cần chuyển đổi.</param>
        public EList(List<T> list)
        {
            _internalList = new List<T>(list);
        }

        /// <summary>
        /// Chuyển một danh sách List thành EList.
        /// </summary>
        /// <param name="list">Danh sách List cần chuyển đổi.</param>
        /// <returns>Danh sách EList.</returns>
        public static EList<T> FromList(List<T> list)
        {
            var eList = new EList<T>();
            foreach (var item in list)
            {
                eList.Add(item);
            }
            return eList;
        }
    }
}
