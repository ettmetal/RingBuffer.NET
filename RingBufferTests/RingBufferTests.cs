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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingBuffer;

namespace RingBufferTests {
    [TestClass]
    public class RingBufferTests {

        private int iterations = 1000;

        private int knownValue = 1;

        /// <summary>
        /// Ensures that size is correctly augmented when items are added.
        /// </summary>
        [TestMethod()]
        public void PutIncrementsSize() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            for(int i = 0; i < iterations; i++) {
                int _tmp = i;
                _buffer.Add(_tmp);
                Assert.AreEqual(i + 1, _buffer.Size, "Size is not equal to number of elements added.");
            }
        }

        /// <summary>
        /// Ensures that size is correctly adjusted when elements are removed.
        /// </summary>
        [TestMethod()]
        public void GetDecrementsSize() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            for(int i = iterations; i > 0; i--) {
                int _tmp = _buffer.Get();
                Assert.AreEqual(i-1, _buffer.Size, "Size does not reflect the correct number of removed elements.");
            }
        }

        /// <summary>
        /// Ensures that capacity expands as needed
        /// </summary>
        [TestMethod()]
        public void CapacityExpands() {
            int _startCapacity = 12;
            RingBuffer<double> _testBuffer = new RingBuffer<double>(_startCapacity);
            for(int i = 0; i < _startCapacity + 1; i++) {
                _testBuffer.Add((double)i);
            }
            Assert.AreEqual(_startCapacity * 2, _testBuffer.Capacity, "Capacity not expanded");
            Assert.AreEqual(_startCapacity + 1, _testBuffer.Size, "incorrect number of elements");
        }

        /// <summary>
        /// Ensures that head/tail move properly by testing the value of data
        /// returned by get.
        /// </summary>
        [TestMethod()]
        public void RetrievedInCorrectOrder() {
            int _iterations = 10;
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            for(int i = 0; i < _iterations; i++) {
                int _tmp = _buffer.Get();
                Assert.AreEqual(i, _tmp, "Incorrect Sequence");
            }
        }

        /// <summary>
        /// Ensures that an exception is thrown when Get() is called on an
        /// empty buffer.
        /// </summary>
        [TestMethod(), ExpectedException(typeof(InvalidOperationException))]
        public void ThrowsError_GetEmpty() {
            RingBuffer<byte> buffer = new RingBuffer<byte>();
            byte _tmp = buffer.Get();
        }

        /// <summary>
        /// Ensures that foreach iteration covers only the range of active
        /// items
        /// </summary>
        [TestMethod()]
        public void CanIterateForeach() {
            RingBuffer<int> buffer = new RingBuffer<int>();
            populateBuffer(iterations, buffer);
            int _iterations = 0;
            foreach(int i in buffer){
                _iterations++;
            }
            Assert.AreEqual(iterations, _iterations, "Wrong number of foreach iterations.");
        }

        /// <summary>
        /// Ensures that the contains function returns the correct value.
        /// </summary>
        [TestMethod()]
        public void ContainsReturnsCorrectly() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            _buffer.Add(knownValue - 1);
            bool _containsKnownValue = _buffer.Contains(knownValue);
            Assert.AreEqual(false, _containsKnownValue);
            populateBuffer(iterations, _buffer);
            _buffer.Add(knownValue);
            _containsKnownValue = _buffer.Contains(knownValue);
            Assert.AreEqual(true, _containsKnownValue);

        }

        /// <summary>
        /// Ensures that after calling Clear(), the RingBuffer contains no
        /// items.
        /// </summary>
        [TestMethod()]
        public void ClearAsExpected() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            _buffer.Clear();
            Assert.AreEqual(0, _buffer.Count);
        }

        /// <summary>
        /// Ensures that CopyTo() behaves properly.
        /// </summary>
        [TestMethod()]
        public void CopyToTest() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            int[] _array = new int[iterations + 1];
            _buffer.CopyTo(_array, 1);
            Assert.AreEqual(default(int), _array[0]);
            for(int i = 1; i < _array.Length; i++) {
                Assert.AreEqual(i - 1, _array[i]);
            }
        }

        /// <summary>
        /// Tests the Size and return value of Contains() after a Remove()
        /// to ensure that the item was removed.
        /// </summary>
        [TestMethod()]
        public void ItemIsRemoved() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            int _preRemoveSize = _buffer.Count;
            _buffer.Remove(0);
            Assert.AreEqual(false, _buffer.Contains(0));
            Assert.AreEqual(_preRemoveSize - 1, _buffer.Count);

        }

        private void populateBuffer(int elements, RingBuffer<int> buffer) {
            for(int i = 0; i < elements; i++) {
                buffer.Add(i);
            }
        }
    }
}
