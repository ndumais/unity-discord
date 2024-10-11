//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Lumpn.Discord
{
    public sealed class Webhook
    {
        private readonly Uri uri;
        public readonly string name;

        public Webhook(string url, string name)
        {
            this.uri = new Uri(url, UriKind.Absolute);
            this.name = name;
        }

        public UnityWebRequest CreateWebRequest(Message message)
        {
            var json = JsonUtility.ToJson(message);

            var downloadHandler = new DownloadHandlerBuffer();
            var uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json))
            {
                contentType = "application/json",
            };

            var request = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST, downloadHandler, uploadHandler);
            return request;
        }

        public UnityWebRequest CreateWebRequest(Message message, byte[] attachmentBytes)
        {
            var sections = new List<IMultipartFormSection>
            {
                CreateDataSection(message),
                CreateFileSection(attachmentBytes,0),
            };

            return UnityWebRequest.Post(uri, sections);
        }

        public UnityWebRequest CreateWebRequest(Message message, IEnumerable<byte[]> multipleAttachmentBytes)
        {
            var sections = new List<IMultipartFormSection>
            {
                CreateDataSection(message),
            };
            sections.AddRange(multipleAttachmentBytes.Select(CreateFileSection));

            return UnityWebRequest.Post(uri, sections);
        }

        private static MultipartFormDataSection CreateDataSection(Message message)
        {
            var json = JsonUtility.ToJson(message);
            return new MultipartFormDataSection("payload_json", json, Encoding.UTF8, "application/json");
        }

        private static MultipartFormFileSection CreateFileSection(byte[] bytes, int index)
        {
            var name = string.Format(CultureInfo.InvariantCulture, "files[{0}]", index);
            return new MultipartFormFileSection(name, bytes, null, "application/octet-stream");
        }
    }
}
