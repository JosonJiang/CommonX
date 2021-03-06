// File generated by hadoop record compiler. Do not edit.
/**
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonX.Logging;
using Org.Apache.Jute;
using Org.Apache.Zookeeper.Data;
using ZooKeeperNet.IO;

namespace Org.Apache.Zookeeper.Proto
{
    public class CreateRequest : IRecord, IComparable
    {
        private static readonly ILogger log;//;
        //private static readonly ILogger log;// = Scope.Resolve<ILoggerFactory>().Create(typeof (CreateRequest));

        public CreateRequest()
        {
        }

        public CreateRequest(string path,byte[] data,IEnumerable<ACL> acl,int flags)
        {
            Path = path;
            Data = data;
            Acl = acl;
            Flags = flags;
        }

        public string Path { get; set; }
        public byte[] Data { get; set; }
        public IEnumerable<ACL> Acl { get; set; }
        public int Flags { get; set; }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing CreateRequest is unimplemented");
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteString(Path, "path");
            a_.WriteBuffer(Data, "data");
            {
                a_.StartVector(Acl, "acl");
                if (Acl != null)
                {
                    foreach (ACL e1 in Acl)
                    {
                        a_.WriteRecord(e1, "e1");
                    }
                }
                a_.EndVector(Acl, "acl");
            }
            a_.WriteInt(Flags, "flags");
            a_.EndRecord(this, tag);
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            Path = a_.ReadString("path");
            Data = a_.ReadBuffer("data");
            {
                IIndex vidx1 = a_.StartVector("acl");
                if (vidx1 != null)
                {
                    var tmpLst = new List<ACL>();
                    for (; !vidx1.Done(); vidx1.Incr())
                    {
                        ACL e1;
                        e1 = new ACL();
                        a_.ReadRecord(e1, "e1");
                        tmpLst.Add(e1);
                    }
                    Acl = tmpLst;
                }
                a_.EndVector("acl");
            }
            Flags = a_.ReadInt("flags");
            a_.EndRecord(tag);
        }

        public override string ToString()
        {
            try
            {
                var ms = new MemoryStream();
                using (var writer =
                    new EndianBinaryWriter(EndianBitConverter.Big, ms, Encoding.UTF8))
                {
                    var a_ =
                        new BinaryOutputArchive(writer);
                    a_.StartRecord(this, string.Empty);
                    a_.WriteString(Path, "path");
                    a_.WriteBuffer(Data, "data");
                    {
                        a_.StartVector(Acl, "acl");
                        if (Acl != null)
                        {
                            foreach (ACL e1 in Acl)
                            {
                                a_.WriteRecord(e1, "e1");
                            }
                        }
                        a_.EndVector(Acl, "acl");
                    }
                    a_.WriteInt(Flags, "flags");
                    a_.EndRecord(this, string.Empty);
                    ms.Position = 0;
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return "ERROR";
        }

        public void Write(EndianBinaryWriter writer)
        {
            var archive = new BinaryOutputArchive(writer);
            Serialize(archive, string.Empty);
        }

        public void ReadFields(EndianBinaryReader reader)
        {
            var archive = new BinaryInputArchive(reader);
            Deserialize(archive, string.Empty);
        }

        public override bool Equals(object obj)
        {
            var peer = (CreateRequest) obj;
            if (peer == null)
            {
                return false;
            }
            if (ReferenceEquals(peer, this))
            {
                return true;
            }
            var ret = false;
            ret = Path.Equals(peer.Path);
            if (!ret) return ret;
            ret = Data.Equals(peer.Data);
            if (!ret) return ret;
            ret = Acl.Equals(peer.Acl);
            if (!ret) return ret;
            ret = (Flags == peer.Flags);
            if (!ret) return ret;
            return ret;
        }

        public override int GetHashCode()
        {
            var result = 17;
            int ret = GetType().GetHashCode();
            result = 37*result + ret;
            ret = Path.GetHashCode();
            result = 37*result + ret;
            ret = Data.GetHashCode();
            result = 37*result + ret;
            ret = Acl.GetHashCode();
            result = 37*result + ret;
            ret = Flags;
            result = 37*result + ret;
            return result;
        }

        public static string Signature()
        {
            return "LCreateRequest(sB[LACL(iLId(ss))]i)";
        }
    }
}