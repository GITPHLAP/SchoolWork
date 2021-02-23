using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCarrier
{
    public enum MessageCarrier { Smtp, VMessage }
    public class MessageSender
    {
        //This class recevie and send SMTP messages AKA SMTP Manager

        Converter converter = new Converter();
        public void sendMessageToAll(MessageCarrier type, string[] to, Message m, bool isHTML)
        {
            if (type.Equals(MessageCarrier.Smtp))
            {
                if (isHTML)
                    m.Body = converter.ConvertBodyToHTML(m.Body);
                //her implementeres alt koden til at sende via Smtp
            }

            if (type.Equals(MessageCarrier.VMessage))
            {
                if (isHTML)
                    m.Body = converter.ConvertBodyToHTML(m.Body);
                //her implementeres alt koden til at sende via VMessage
            }
        }

        public void sendMessage(MessageCarrier type, Message m, bool isHTML)
        {
            //herinde sendes der en email ud til modtageren
            if (type.Equals(MessageCarrier.Smtp))
            {
                if (isHTML)
                    m.Body = converter.ConvertBodyToHTML(m.Body);
                //her implementeres alt koden til at sende via Smtp
            }

            if (type.Equals(MessageCarrier.VMessage))
            {
                if (isHTML)
                    m.Body = converter.ConvertBodyToHTML(m.Body);
                //her implementeres alt koden til at sende via VMessage
            }
        }
    }
}
