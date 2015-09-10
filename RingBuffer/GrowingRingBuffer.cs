#region License
/* Copyright 2015 Joe Osborne
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
    /// A generic ring buffer. Grows when capacity is reached.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the buffer</typeparam>
    public class GrowingRingBuffer<T> : RingBuffer<T> {

        private int originalCapacity;

        /// <summary>
        /// Adds an item to the end of the buffer.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        public new void Put(T item) {
            // If tail & head are equal and the buffer is not empty, assume
            // that it would overflow and expand the capacity before adding the
            // item.
            if(tail == head && size != 0) {
                T[] _newArray = new T[buffer.Length + originalCapacity];
                for(int i = 0; i < Capacity; i++) {
                    _newArray[i] = buffer[i];
                }
                buffer = _newArray;
                tail = (head + size) % Capacity;
                addToBuffer(item);
            }
            // If the buffer would not overflow, just add the item.
            else {
                addToBuffer(item);
            }
        }

        #region Constructors
        /// <summary>
        /// Creates a new GrowingRingBuffer with capacity of 4.
        /// </summary>
        public GrowingRingBuffer() : this(4) { }

        /// <summary>
        /// Creates a new GrowingRingBuffer.
        /// </summary>
        /// <param name="startCapacity">The initial capacity of the buffer.</param>
        public GrowingRingBuffer(int startCapacity)
            : base(startCapacity) {
            originalCapacity = startCapacity;
        }
        #endregion
    }
}
