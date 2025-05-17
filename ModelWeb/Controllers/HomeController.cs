using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelWeb.Models;
using Services.Data.Models;
using Services.Data.Repository;
using System;
using System.Diagnostics;
using System.Linq;
using static ModelWeb.Controllers.HomeController;

namespace ModelWeb.Controllers
{
	[Authorize]
	//[Authorize][Authorize(Roles = "user")]
	//[Authorize(Roles = "admin,Manager")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Item> _repository;

        public HomeController(ILogger<HomeController> logger, IRepository<Item> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult>  Index()
        {

			var UserID = HttpContext.User.FindFirst("UserId").Value;
			var IetmData = await _repository.GetAllLazyLoad(x => x.ItemInUse == true); 

            ViewBag.ItemList = IetmData.Select(x => new SelectListItem
            {
                Value = x.ItemId.ToString(),
                Text = x.ItemName
            }).ToList();



			return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTable(string tableName, List<int> Items, int coulmCount)
        {

            var NumbersList = LoadNumbers(coulmCount);
            var RowsCount = Items.Count() / coulmCount;
            int RowNo = 1;
            List<SelectedItems> ItemsList = new List<SelectedItems>();
            while (RowsCount > RowNo)
            {


                foreach (var x in NumbersList)
                {
                    var ListItemS = ItemsList.Select(t => t.ItemId).ToList();

					SelectedItems selectedItems = new SelectedItems();
                    selectedItems.ItemId = SelecRandomItem(Items.Where(d=> !ListItemS.Contains(d)).ToList());
                    selectedItems.ColNo = x;
                    selectedItems.RowNo = RowNo;
                    ItemsList.Add(selectedItems);
                }
                RowNo++;
            }
            var IetmData = await _repository.GetAllLazyLoad(x => x.ItemInUse == true);
            var SelectRows= ItemsList.Select(x=>x.RowNo).Distinct().ToList();

            var Rdata = new
            {
                ColList = NumbersList,
                Details = SelectRows.Select(r=> new
                {
					RowNo= r,
                  RowData=  ItemsList.Where(x=>x.RowNo==r).Select(x => new
                    {
                        x.RowNo,
                        x.ColNo,
                        x.ItemId,
                        ItemName = IetmData.Where(s => s.ItemId == x.ItemId).Select(s => s.ItemName).FirstOrDefault(),


                    }).ToList()
                }
                )
            };
       


			return Ok(Rdata);


		}

    
		
		public class SelectedItems
        {
            public int ColNo { get; set; }
            public int RowNo { get; set; }
			public int ItemId { get; set; }
		}
        public int SelecRandomItem(List<int> Items)
        {
		
			Random random = new Random();
			int randomIndex = random.Next(Items.Count);  // رقم عشوائي من 0 إلى Count-1
			int randomNumber = Items[randomIndex];
            return randomNumber;
		}
        public List<int> LoadNumbers(int coulmCount)
        {
            List<int> numbers = new List<int>();
            while (numbers.Count() < coulmCount)
            {
                Random random = new Random();

                int number = random.Next(10, 99);

                if (!numbers.Contains(number))
                {
                    numbers.Add(number);
                }
            }
            return numbers;
        }


	

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
