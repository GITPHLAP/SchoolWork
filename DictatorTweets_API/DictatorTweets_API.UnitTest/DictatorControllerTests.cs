using DictatorTweets_API.Controllers;
using DictatorTweets_API.DAL;
using DictatorTweets_API.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DictatorTweets_API.UnitTest
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_GetDictator_ReturnTaskListDictator()
        {
            //Arrange
            Mock<IDictatorDB> dbMock = new Mock<IDictatorDB>();
            DictatorController dictatorController = new DictatorController(dbMock.Object);

            dbMock
                .Setup(d => d.GetDictators())
                .Returns(Task.Run(() => GetMockDictators()));

            //Act
            var result = dictatorController.Get();

            //Assert
            var dictatorResult = result.Result;
            for (int i = 0; i < dictatorResult.Count; i++)
            {
                Assert.AreEqual(dictatorResult[i].Id, GetMockDictators()[i].Id);
                Assert.AreEqual(dictatorResult[i].FirstName, GetMockDictators()[i].FirstName);
                Assert.AreEqual(dictatorResult[i].LastName, GetMockDictators()[i].LastName);
                Assert.AreEqual(dictatorResult[i].BirthYear, GetMockDictators()[i].BirthYear);
                Assert.AreEqual(dictatorResult[i].DeadYear, GetMockDictators()[i].DeadYear);
                Assert.AreEqual(dictatorResult[i].ShortDescription, GetMockDictators()[i].ShortDescription);
                Assert.AreEqual(dictatorResult[i].MappningText, GetMockDictators()[i].MappningText);
            }
            dbMock.Verify(d => d.GetDictators(), Times.Once());
        }

        [Test]
        public void Post_GetDictator_ReturnTaskDictator()
        {
            //Arrange
            Mock<IDictatorDB> dbMock = new Mock<IDictatorDB>();
            DictatorController dictatorController = new DictatorController(dbMock.Object);

            Dictator rndDic = GetRandomDictator();
            dbMock
                .Setup(d => d.CreateDictator(rndDic))
                .Returns(Task.Run(() => rndDic));

            //Act
            var result = dictatorController.Post(rndDic);

            //Assert
            var dictatorResult = result.Result;
            Assert.AreEqual(dictatorResult.Id, rndDic.Id);
            Assert.AreEqual(dictatorResult.FirstName, rndDic.FirstName);
            Assert.AreEqual(dictatorResult.LastName, rndDic.LastName);
            Assert.AreEqual(dictatorResult.BirthYear, rndDic.BirthYear);
            Assert.AreEqual(dictatorResult.DeadYear, rndDic.DeadYear);
            Assert.AreEqual(dictatorResult.ShortDescription, rndDic.ShortDescription);
            Assert.AreEqual(dictatorResult.MappningText, rndDic.MappningText);
            dbMock.Verify(d => d.CreateDictator(It.IsAny<Dictator>()), Times.Once());
        }

        [Test]
        public void Put_GetDictator()
        {
            //Arrange


            foreach (var item in GetMockDictators())
            {
                Mock<IDictatorDB> dbMock = new Mock<IDictatorDB>();
                DictatorController dictatorController = new DictatorController(dbMock.Object);
                Dictator rndDic = item;
            dbMock
                .Setup(d => d.UpdateDictator(rndDic));

                //Act
                dictatorController.Put(rndDic);

                //Assert

                dbMock.Verify(d => d.UpdateDictator(It.IsAny<Dictator>()), Times.Once());
            }

        }

        [Test]
        public void Delete_GetDictator()
        {
            //Arrange


            foreach (var item in GetMockDictators())
            {
                Mock<IDictatorDB> dbMock = new Mock<IDictatorDB>();
                DictatorController dictatorController = new DictatorController(dbMock.Object);
                Dictator rndDic = item;
                dbMock
                    .Setup(d => d.DeleteDictator(rndDic));

                //Act
                dictatorController.Delete(rndDic);

                //Assert

                dbMock.Verify(d => d.DeleteDictator(It.IsAny<Dictator>()), Times.Once());
            }
        }


        #region Mock Dictators 

        private List<Dictator> GetMockDictators()
        {
            using (StreamReader r = new StreamReader("mockData.json"))
            {
                
                return JsonConvert.DeserializeObject<List<Dictator>>(r.ReadToEnd());
            }
        }

        private Dictator GetRandomDictator()
        {
            Random r = new();
            var dictators = GetMockDictators();
            return dictators[r.Next(dictators.Count)];
        }

        #endregion
    }
}