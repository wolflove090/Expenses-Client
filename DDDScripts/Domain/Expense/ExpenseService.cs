using System.Collections;
using System.Collections.Generic;
using Expense.Models.Records;
using Expense.Models.Categories;

namespace Expense.Service
{
    public class ExpenseService
    {
        IRecordRepository recordRepository;
        Category[] expenseCategories;

        public ExpenseService(IRecordRepository recordRepository)
        {
            this.recordRepository = recordRepository;

            // 家計簿対象のカテゴリを作成
            this.expenseCategories = this._CreateCategories();
        }

        public Record GetSumExpenseRecord()
        {
            Record[] records = this.recordRepository.FindAll();
            return Record.Sum(records, "合計");
        }

        public Category[] GetCategories()
        {
            return this.expenseCategories;
        }

        Category[] _CreateCategories()
        {
            List<Category> list = new List<Category>();
            list.Add(new Category("食費", "btn_line_black_cart"));
            list.Add(new Category("コストコ", "btn_line_black_shopping_2"));
            list.Add(new Category("ガソリン", "btn_line_black_potion"));
            list.Add(new Category("日用品", "btn_line_black_home"));
            list.Add(new Category("外食", "btn_line_black_shop"));
            list.Add(new Category("コンビニ", "btn_line_black_restore"));
            list.Add(new Category("お昼", "btn_line_black_bag"));
            list.Add(new Category("美容・服飾", "btn_line_black_jewel_1"));
            list.Add(new Category("医療・健康", "btn_line_black_life"));
            list.Add(new Category("ゲーム", "btn_line_black_game_1"));
            list.Add(new Category("娯楽・レジャー", "btn_line_black_flag"));
            list.Add(new Category("教養", "btn_line_black_lamp"));
            list.Add(new Category("交際費", "btn_line_black_gift"));
            list.Add(new Category("樹私用", "btn_line_black_man"));
            list.Add(new Category("亜紀私用", "btn_line_black_woman"));
            
            return list.ToArray();
        }
    }
}

