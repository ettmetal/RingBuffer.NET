# RingBuffer
By [Joe Osborne](www.joeo.rocks)

## Description
RingBuffer is a simple C# implementation of a ring (circular) buffer for C#. It is generic, and implements the .Net IEnumerable and IEnumerable<T> interfaces.

## Usage
	RingBuffer<int> buffer = new RingBuffer<int>(); // Declares and initialises a new integer ring buffer with default size (4).

	buffer.Put(3); // Adds 3 as an item to the buffer

	int result = buffer.Get(); // Declares variable result and assigns it to the oldest element in the buffer. Removes this element from the buffer.

	buffer.Size; // The number of elements currently contained in the buffer.

	buffer.Capacity; // The number of elements the buffer can store.

	foreach(int item in buffer){ //Sets each element stored in the buffer to 0.
		i = 0;
	}

### Including in a Project
RingBuffer can be built as is to create a .dll which can be referenced by any other project. RingBuffer.cs can also be included in any project and will allow use of the RingBuffer<T> class.

## Contributing
All suggestions are welcome, in the form of pull requests or messages.

## License
RingBuffer is released under the GNU GPL V3.0
