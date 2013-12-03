﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml;

using ddex.net.xml.ern.Item37D2;
using DDEX.ERN37.MessagingClassLibrary;

namespace DDEX.ERN37.MessagingClassLibrary.Samples
{
    [TestFixture]
    public class BasicValidationAndSerialisationTests
    {
        [Test]
        public void MinimumValidMessage()
        {
            var msg = new NewReleaseMessage();

            msg.LanguageAndScriptCode = "en";
            msg.MessageSchemaVersionId = Helpers.MessageSchemaVersionId;
            msg.MessageHeader = new MessageHeader
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageFileName = "test.xml",
                MessageCreatedDateTime = DateTime.UtcNow,
                MessageSender = new MessagingParty
                {
                    PartyId = { new PartyId() }
                },
                MessageRecipient = new MessagingParty
                {
                    PartyId = { new PartyId() }
                }
            };

            msg.ResourceList = new ResourceList();
            msg.ReleaseList = new ReleaseList();

            msg.ResourceList.SoundRecording.Add(new SoundRecording
                {
                    SoundRecordingDetailsByTerritory = 
                    { 
                        new SoundRecordingDetailsByTerritory
                        {
                            TerritoryCode = { new CurrentTerritoryCode { TypedValue = AllowedValues.DdexTerritoryCodeValues._Worldwide } },                            
                        },
                    },
                    SoundRecordingType = new SoundRecordingType
                    {
                        TypedValue = AllowedValues.SoundRecordingTypeValues._NonMusicalWorkReadalongSoundRecording
                    },
                    SoundRecordingId =
                    {
                        new SoundRecordingId
                        {
                            ISRC = "666"
                        }
                    }
                });

            foreach (var error in msg.Validate(Helpers.ErnNamespace, Helpers.ErnSchemaUrl).Messages)
            {
                Console.WriteLine(error);
            }

            msg.Untyped.Add(new XAttribute(XNamespace.Xmlns + Helpers.ErnNamespacePrefix, Helpers.ErnNamespace));
            Console.Write(msg.Untyped.ToString(SaveOptions.None));
        }
    }
}
