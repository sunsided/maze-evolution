using System;
using System.Runtime.Serialization;

namespace Evolution
{
	/// <summary>
	/// Exception, die vom Generator geworfen wird, wenn der Klasse das Markierungsattribut fehlt
	/// </summary>
	/// <remarks></remarks>
	[Serializable]
	public class ClassDecorationException : GeneratorException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		/// <remarks></remarks>
		public ClassDecorationException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneratorException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <remarks></remarks>
		public ClassDecorationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GeneratorException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		/// <remarks></remarks>
		public ClassDecorationException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Exception"/> class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
		///   
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
		/// <remarks></remarks>
		protected ClassDecorationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}
