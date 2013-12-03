using NUnit.Framework;
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
                MessageFileName = "Profile_AudioSingle.xml",
                MessageCreatedDateTime = DateTime.UtcNow,
                MessageControlType = AllowedValues.MessageControlTypeValues._TestMessage,
                MessageSender = new MessagingParty
                {
                    PartyId = 
                    { 
                        new PartyId
                        {
                            IsDPID = true,
                            TypedValue = "PADPIDA0000000001A"
                        }                        
                    },
                    TradingName = new Name
                    {
                        TypedValue = "Iron Crown Music"
                    }
                },
                MessageRecipient = new MessagingParty
                {
                    PartyId = 
                    { 
                        new PartyId
                        {
                            IsDPID = true,
                            TypedValue = "PADPIDA0000000002A"
                        }                        
                    },
                    TradingName = new Name
                    {
                        TypedValue = "Lamson Digital Distribution"
                    }
                }
            };

            msg.ResourceList = new ResourceList();
            msg.ReleaseList = new ReleaseList();

            msg.ResourceList.SoundRecording.Add(new SoundRecording
                {
                    ResourceReference = "A1",
                    ReferenceTitle = new ReferenceTitle
                    {
                        TitleText = new TitleText
                        {
                            TypedValue = "Can you feel ...the Monkey Claw!"
                        }
                    },
                    SoundRecordingType = new SoundRecordingType
                    {
                        TypedValue = AllowedValues.SoundRecordingTypeValues._MusicalWorkSoundRecording
                    },
                    SoundRecordingId =
                    {
                        new SoundRecordingId
                        {
                            ISRC = "666"
                        }
                    },
                    Duration = new TimeSpan(0, 13, 31),
                    SoundRecordingDetailsByTerritory =
                    {
                        new SoundRecordingDetailsByTerritory
                        {
                            TerritoryCode = { new CurrentTerritoryCode { TypedValue = AllowedValues.DdexTerritoryCodeValues._Worldwide } },
                            Title = 
                            { 
                                new Title 
                                {
                                    TitleType = AllowedValues.TitleTypeValues._FormalTitle,
                                    TitleText = new TitleText 
                                    {
                                        TypedValue = "Can you feel ...the Monkey Claw!"
                                    }
                                },
                                new Title 
                                {
                                    TitleType = AllowedValues.TitleTypeValues._DisplayTitle,
                                    TitleText = new TitleText 
                                    {
                                        TypedValue = "Can you feel ...the Monkey Claw!"
                                    }
                                },
                            },
                        },
                    },
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
