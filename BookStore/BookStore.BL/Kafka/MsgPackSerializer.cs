﻿using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Kafka
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}