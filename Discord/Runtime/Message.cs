﻿//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;

namespace Lumpn.Discord
{
    /// <summary>
    /// https://discord.com/developers/docs/resources/webhook#execute-webhook-jsonform-params
    /// </summary>
    [Serializable]
    public struct Message
    {
        /// <summary>
        /// Overwrites the username of the webhook. Recommended to leave blank. (optional)
        /// </summary>
        public string username;

        /// <summary>
        /// Message text. Recommended to use embeds instead. (optional)
        /// </summary>
        public string content;

        /// <summary>
        /// Embedded messages. (optional)
        /// </summary>
        public Embed[] embeds;
    }
}
