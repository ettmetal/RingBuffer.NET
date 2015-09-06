# RingBuffer
By [Joe Osborne](www.joeo.rocks)

## Description
RingBuffer is a simple C# implementation of a ring (circular) buffer for C#. It is generic, and implements the .Net IEnumerable and IEnumerable<T> interfaces.

## Usage
	using RingBuffer; // The RingBuffer<T> class is in the RingBuffer namespace.

	//...

	RingBuffer<int> buffer = new RingBuffer<int>(); // Declares and initialises a new integer ring buffer with default size (4).

	buffer.Put(3); // Adds 3 as an item to the buffer

	int result = buffer.Get(); // Declares variable result and assigns it to the oldest element in the buffer. Removes this element from the buffer.

	buffer.Size; // The number of elements currently contained in the buffer.

	buffer.Capacity; // The number of elements the buffer can store.

	foreach(int item in buffer){ //Sets each element stored in the buffer to 0.
		i = 0;
	}

### Including in a Project
Releases on GitHub include a .dll including the RingBuffer functionality, which can be included as a build reference in any project.

RingBuffer can also be built as is to create a .dll which can be referenced by any other project. RingBuffer.cs can also be included in any project and will allow use of the RingBuffer<T> class.

### Requirements
RingBuffer is built targeting the .NET framework V3.5. As such, the release .dll will only work in projects targeting at least .NET 3.5. The code should however work with .NET 2.0 onwards.

## Contributing
All suggestions are welcome, in the form of pull requests or messages.

## License
RingBuffer is released under the GNU GPL V3.0
