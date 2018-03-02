using System;
using DataAccess.BLL;
using DataAccess.Repository;
using DataAccess.RepositoryContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc.Controllers;
using Service.Contracts;
using Moq;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;
using Service.Services;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {


            List<Member> member = new List<Member>();


            var repositoryMock = new Mock<IMemberService>();

            repositoryMock.Setup(r => r.GetMembers()).Returns(member);

            var controller = new MemberController(repositoryMock.Object);

            ViewResult result = (ViewResult)controller.Index(1);


            if (result.ViewName == "")
                result.ViewName = "Index";


            Assert.AreEqual(result.ViewName, "Index");
            Assert.AreEqual(result.Model, member.ToPagedList(1,10));
            repositoryMock.VerifyAll();

        }



    }
}
