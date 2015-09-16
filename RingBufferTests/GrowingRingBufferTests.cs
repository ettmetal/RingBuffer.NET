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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingBuffer;

namespace RingBufferTests {
    [TestClass]
    public class GrowingRingBufferTests {
        [TestMethod]
        public void GrowsOnOverflow() {
            int _initialCapacity = 5;
            int _elements = 11;
            GrowingRingBuffer<int> _buffer = new GrowingRingBuffer<int>(_initialCapacity);
            populateBuffer(_elements, _buffer);
            int _timesIncremented = _elements / _initialCapacity;
            if(_elements % _initialCapacity != 0) _timesIncremented++;
            int _expectedCapacity = (_timesIncremented * _initialCapacity);
            Assert.AreEqual(_expectedCapacity, _buffer.Capacity);
        }

        private void populateBuffer(int elements, GrowingRingBuffer<int> buffer) {
            for(int i = 0; i < elements; i++) {
                buffer.Put(i);
            }
        }
    }
}
