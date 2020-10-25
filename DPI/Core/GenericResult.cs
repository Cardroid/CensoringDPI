using System;
using System.Collections.Generic;
using System.Text;

namespace GoodByeDPIDotNet.Core
{
    public struct GenericResult<T>
    {
        public GenericResult(bool success)
        {
            this.Success = success;
            this.Result = default;
        }
        public GenericResult(bool success, T result)
        {
            this.Success = success;
            this.Result = result;
        }

        public static implicit operator bool(GenericResult<T> d) => d.Success;
        public override string ToString() => Result.ToString();

        /// <summary>
        /// 성공여부
        /// </summary>
        public bool Success { get; }
        /// <summary>
        /// 작업 결과
        /// </summary>
        public T Result { get; }
    }
}
