using System;

namespace ShpConverter
{
    /// <summary>
    /// ShpConvert异常
    /// </summary>
    [Serializable]
    public class ShpConvertException : Exception
    {
        /// <summary>
        /// 初始化ShpConvert的异常类新实例。
        /// </summary>
        public ShpConvertException(): base() {}

        /// <summary>
        /// 初始化ShpConvert的异常类新实例，并带有指定错误消息。
        /// </summary>
        /// <param name="message">描述错误的信息</param>
        public ShpConvertException(string message) : base(message) {}

        /// <summary>
        /// 初始化ShpConvert的异常类新实例，并带有指定错误消息和对内部异常的引用。
        /// </summary>
        /// <param name="message">描述错误的信息</param>
        /// <param name="innerException">内部异常</param>
        public ShpConvertException(string message, Exception innerException) : base(message, innerException) {}
        
    }
}
