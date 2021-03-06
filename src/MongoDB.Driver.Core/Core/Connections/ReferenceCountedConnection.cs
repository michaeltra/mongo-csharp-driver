﻿/* Copyright 2013-2014 MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Threading;

namespace MongoDB.Driver.Core.Connections
{
    /// <summary>
    /// Represents a reference counted connection.
    /// </summary>
    public class ReferenceCountedConnection : ConnectionWrapper
    {
        // fields
        private int _referenceCount = 1;
 
        // constructors
        public ReferenceCountedConnection(IConnection wrapped)
            : base(wrapped)
        {
        }

        // methods
        public void DecrementReferenceCount()
        {
            ThrowIfDisposed();
            var referenceCount = Interlocked.Decrement(ref _referenceCount);
            if (referenceCount == 0)
            {
                Dispose();
            }
        }

        public void IncrementReferenceCount()
        {
            ThrowIfDisposed();
            Interlocked.Increment(ref _referenceCount);
        }
    }
}
