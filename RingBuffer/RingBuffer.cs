#region License
/* Copyright 2014 Joe Osborne
 * 
 * This file is part of RingBuffer.
 *
 *  RingBuffer is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  RingBuffer is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with RingBuffer. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion
using System;
using System.Collections;
using System.Collections.Generic;

namespace RingBuffer {
    /// <summary>
    /// A generic ring buffer. Grows when
    /// </summary>
    /// <typeparam name="T">The type of data stored in the buffer</typeparam>
    public class RingBuffer<T> : IEnumerable<T>, IEnumerable {

        private int head = 0;
        private int tail = 0;
        private int size = 0;

        private T[] buffer;

        /// <summary>
        /// The total number of elements the buffer can store (grows).
        /// </summary>
        public int Capacity { get { return buffer.Length; } }

        /// <summary>
        /// The number of elements currently contained in the buffer.
        /// </summary>
        public int Size { get { return size; } }

        //==Buffer Access======================================================
        /// <summary>
        /// Retrieve the next item from the buffer.
        /// </summary>
        /// <returns>The oldest item added to the buffer.</returns>
        public T Get() {
            if(size == 0) throw new System.InvalidOperationException("Buffer is empty.");
            T _item = buffer[head];
            head = (head + 1) % Capacity;
            size--;
            return _item;
        }

        /// <summary>
        /// Adds an item to the end of the buffer.
        /// </summary>
        /// <param name="toAdd">The item to be added.</param>
        public void Put(T toAdd) {
            if(tail == head && size != 0) {
                T[] _newArray = new T[buffer.Length * 2];
                for(int i = 0; i < Capacity; i++) {
                    _newArray[i] = buffer[i];
                }
                buffer = _newArray;
                tail = (head + size) % Capacity;
                addToBuffer(toAdd);
            }
            else {
                addToBuffer(toAdd);
            }
        }

        // So we can be DRY
        private void addToBuffer(T toAdd) {
            buffer[tail] = toAdd;
            tail = (tail + 1) % Capacity;
            size++;
        }

        //==Constructors=======================================================
        /// <summary>
        /// Creates a new RingBuffer with capacity of 4.
        /// </summary>
        public RingBuffer() : this(4) { }

        /// <summary>
        /// Creates a new RingBuffer.
        /// </summary>
        /// <param name="startCapacity">The initial capacity of the buffer.</param>
        public RingBuffer(int startCapacity) {
            buffer = new T[startCapacity];
        }

        //==IEnumerable<T> Methods=============================================

        public IEnumerator<T> GetEnumerator() {
            int _index = head;
            for(int i = 0; i < size; i++, _index = (_index + 1) % Capacity) {
                yield return buffer[_index];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)GetEnumerator();
        }
    }
}
