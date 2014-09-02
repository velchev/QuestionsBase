namespace QuestionsBase.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using NUnit.Framework;
    using QuestionBase.Services.Services;
    using QuestionsBase.Controllers;
    using Repository.Entities;
    using Rhino.Mocks;

    [TestFixture]
    public class QuestionContollerTest
    {
        private IQuestionService _questionService;
        private IQuestionTypeService _questionTypeService;
        QuestionController _target;
        private new List<QuestionTypeEntity> _questionTypes;
        private new List<QuestionEntity> _questions;
        [SetUp]
        public void Setup()
        {
            _questionService = MockRepository.GenerateMock<IQuestionService>();
            _questionTypeService = MockRepository.GenerateMock<IQuestionTypeService>();
            _target = new QuestionController(_questionService, _questionTypeService);

            _questionTypes = new List<QuestionTypeEntity>
                    {
                        new QuestionTypeEntity {Id = 1, Type = "MVC"},
                        new QuestionTypeEntity {Id = 2, Type = "OOP"}
                    };
            _questions = new List<QuestionEntity>
                                {
                                    new QuestionEntity
                                        {
                                            Id = 1,
                                            Title = "Question1",
                                            DifficultyLevel = 2,
                                            Answer = "Answer1",
                                            QuestionTypeEntityId = 2
                                        },
                                    new QuestionEntity
                                        {
                                            Id = 2,
                                            Title = "Question2",
                                            DifficultyLevel = 1,
                                            Answer = "Answer2",
                                            QuestionTypeEntityId = 1
                                        }
                                };

            _questionService.Expect(s => s.GetAllQuestions()).Return(_questions);
            _questionTypeService.Expect(s => s.GetAllQuestionTypes()).Return(_questionTypes);

            _questionService.Expect(s => s.FindById(1)).Return(_questions[0]);
        }


        [TestCase]
        public void ConstructorReturnNotNullInstance()
        {
            Assert.IsNotNull(_target);
        }

        [TestCase]
        public void IndexReturnsNotNullView()
        {
            var result = _target.Index();
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(2, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }


        [TestCase]
        public void FilterReturnsNotNullView()
        {
            var result = _target.Filter(1);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(1, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }

        [TestCase]
        public void FilterWithNoParameterReturnsNotNullView()
        {
            var result = _target.Filter();
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(2, ((IEnumerable<QuestionEntity>)view.Model).Count());
        }

        [TestCase]
        public void DetailsReturnsOneItemTest()
        {
            var result = _target.Details(1);
            var view = result as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Assert.AreEqual(1, ((QuestionEntity)view.Model).Id);
        }

        [TestCase]
        public void CreateNotNull()
        {
            var result = _target.Create();
            var view = result as ViewResult;

            Assert.NotNull(view);
        }



        [TestCase]
        public void CreatePostNotNull()
        {
            var result = _target.Create(_questions[0]);
            //var view = result as ViewResult;

            //Assert.NotNull(view);
            //Assert.NotNull(view.Model);

            //Assert.AreEqual(_questions[0], view.Model);

        }
    }
}
