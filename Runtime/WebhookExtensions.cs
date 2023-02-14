//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections;
using UnityEngine;

namespace Lumpn.Discord
{
    public static class WebhookExtensions
    {
        public static IEnumerator Send(this Webhook webhook, string text)
        {
            var message = new Message
            {
                content = text,
            };

            return webhook.Send(message);
        }

        public static IEnumerator Send(this Webhook webhook, Embed embed)
        {
            var message = new Message
            {
                embeds = new[] { embed },
            };

            return webhook.Send(message);
        }

        public static IEnumerator Send(this Webhook webhook, Message message)
        {
            using (var request = webhook.CreateWebRequest(message))
            {
                yield return request.SendWebRequest();

                Debug.AssertFormat(!request.isHttpError, "{0}: {1}", request.error, request.downloadHandler.text);
            }
        }
    }
}
