using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KeqingNiuza.Wish.Const;

namespace KeqingNiuza.Wish
{
    /// <summary>
    /// 成就计算方法
    /// </summary>
    static class AchievementComputeMethod
    {

        #region 欧非


        /// <summary>
        /// 85抽之后才出5星
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        public static void 非酋竟是我自己(List<WishData> datas, List<AchievementInfo> infos)
        {
            var list = datas.OrderByDescending(x => x.Guarantee).ThenBy(x => x.Time);
            var a = list.First();
            if (a.Guarantee > 84)
            {
                infos.Add(new AchievementInfo
                {
                    Name = $"非酋竟是我自己",
                    Description = $"85抽之后才出5星",
                    IsFinished = true,
                    FinishTime = a.Time,
                    Total = $"总计 {a.Guarantee}"
                });
            }
        }


        /// <summary>
        /// 十连两金及以上
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        public static void 我就是欧皇(List<WishData> datas, List<AchievementInfo> infos)
        {
            var list = datas.GroupBy(x => x.Time).Where(g => g.Count(x => x.Rank == 5) >= 2).OrderBy(g => g.Key);
            if (list.Any())
            {
                var info = new AchievementInfo
                {
                    Name = "我就是欧皇！",
                    Description = "十连两金及以上",
                    Comment = "",
                    IsFinished = true,
                    FinishTime = list.First().Key,
                    Total = $"总计 {list.Count()} 次",
                };
                infos.Add(info);
            }
        }



        #endregion




        #region 最高记录


        /// <summary>
        /// 抽到最多的5星角色
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 五星角色爱你哟(List<WishData> datas, List<AchievementInfo> infos)
        {
            var groups = datas.Where(x => x.ItemType == "角色" && x.Rank == 5).GroupBy(x => x.Name);
            groups = groups.OrderByDescending(x => x.Count());
            var n = groups.First().Count();
            foreach (var group in groups)
            {
                if (group.Count() == n)
                {
                    var list = group.OrderByDescending(x => x.Time);
                    var a = list.First();
                    var info = new AchievementInfo
                    {
                        Name = $"「{a.Name}」爱你哟",
                        Description = $"抽到最多的5星角色是「{a.Name}」",
                        IsFinished = true,
                        FinishTime = a.Time,
                        Total = $"总计 {list.Count()}",
                    };
                    infos.Add(info);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 抽到最多的4星角色
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 四星角色爱你哟(List<WishData> datas, List<AchievementInfo> infos)
        {
            var groups = datas.Where(x => x.ItemType == "角色" && x.Rank == 4).GroupBy(x => x.Name);
            groups = groups.OrderByDescending(x => x.Count());
            var n = groups.First().Count();
            foreach (var group in groups)
            {
                if (group.Count() == n)
                {
                    var list = group.OrderByDescending(x => x.Time);
                    var a = list.First();
                    var info = new AchievementInfo
                    {
                        Name = $"「{a.Name}」爱你哟",
                        Description = $"抽到最多的4星角色是「{a.Name}」",
                        IsFinished = true,
                        FinishTime = a.Time,
                        Total = $"总计 {list.Count()}"
                    };
                    infos.Add(info);
                }
                else
                {
                    break;
                }
            }
        }


        /// <summary>
        /// 一天抽卡达到78次
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        public static void 一掷千金(List<WishData> datas, List<AchievementInfo> infos)
        {
            var group = datas.GroupBy(x => x.Time.Date).OrderByDescending(x => x.Count()).First();
            if (group.Count() >= 78)
            {
                var info = new AchievementInfo
                {
                    Name = "一掷千金",
                    Description = "一天抽卡达到78次",
                    //todo 成就一掷千金的完成评论
                    Comment = "",
                    IsFinished = true,
                    FinishTime = group.Key,
                    Total = $"总计 {group.Count()}",
                };
                infos.Add(info);
            }
            else
            {
                var info = new AchievementInfo
                {
                    Name = "一掷千金",
                    Description = "一天抽卡达到78次",
                    Comment = "",
                    IsFinished = false,
                    Progress = $"{group.Count() / 78}",
                };
                infos.Add(info);
            }
        }


        /// <summary>
        /// 累计20天没有进行活动祈愿
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        public static void 仓鼠(List<WishData> datas, List<AchievementInfo> infos)
        {
            var list = datas.Where(x => x.WishType == WishType.CharacterEvent || x.WishType == WishType.WeaponEvent).GroupBy(x => x.Time.Date).OrderBy(g => g.Key).ToList();
            if (list.Count == 1)
            {
                return;
            }
            var span = list[1].Key - list[0].Key;
            var time = list[1].Key;
            for (int i = 1; i < list.Count; i++)
            {
                var tmp = list[i].Key - list[i - 1].Key;
                if (tmp > span)
                {
                    span = tmp;
                    time = list[i].Key;
                }
            }
            var info = new AchievementInfo
            {
                Name = "仓鼠",
                Description = "累计超过20天没有进行活动祈愿",
                //todo Comment = "原石的数量，令人安心",
                IsFinished = true,
                FinishTime = time,
                Total = $"总计 {span.Days}",
            };
            infos.Add(info);
        }


        #endregion



        #region 满命







        #endregion



        #region 其他


        /// <summary>
        /// 在一次「赤团开时」活动祈愿中获取三个胡桃
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 三个胡桃(List<WishData> datas, List<AchievementInfo> infos)
        {
            var events = WishEventList.FindAll(x => x.Name == "赤团开时");
            foreach (var item in events)
            {
                var list = datas.Where(x => x.Time >= item.StartTime && x.Time <= item.EndTime).Select(x => x);
                list = list.Where(x => x.Name == "胡桃").Select(x => x);
                if (list.Count() >= 3)
                {
                    var info = new AchievementInfo
                    {
                        Name = "一个胡桃，两个胡桃，三个胡桃",
                        Description = "在一次「赤团开时」活动祈愿中获取三个胡桃",
                        IsFinished = true,
                        FinishTime = list.ElementAt(2).Time,
                        Total = $"总计 {list.Count()}",
                    };
                    infos.Add(info);
                }
            }

        }


        /// <summary>
        /// 在一次「浮生孰来」活动祈愿中抽中「甘雨」和「七七」
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 椰羊的奶好喝(List<WishData> datas, List<AchievementInfo> infos)
        {
            var events = WishEventList.FindAll(x => x.Name == "浮生孰来");
            foreach (var item in events)
            {
                var list = datas.Where(x => x.Time >= item.StartTime && x.Time <= item.EndTime);
                list = list.Where(x => x.Name == "甘雨" || x.Name == "七七").Select(x => x);
                bool hasGanyu = false, hasQiqi = false;
                foreach (var data in list)
                {
                    if (data.Name == "甘雨")
                    {
                        hasGanyu = true;
                    }
                    else
                    {
                        hasQiqi = true;
                    }
                    if (hasGanyu && hasQiqi)
                    {
                        var info = new AchievementInfo
                        {
                            Name = "「椰羊的奶，好喝！」",
                            Description = "在一次「浮生孰来」活动祈愿中抽中「甘雨」和「七七」",
                            IsFinished = true,
                            FinishTime = data.Time,
                        };
                        infos.Add(info);
                        break;
                    }
                }
            }

        }


        /// <summary>
        /// 累计抽出7把狼的末路
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 七匹狼(List<WishData> datas, List<AchievementInfo> infos)
        {
            var list = datas.Where(x => x.Name == "狼的末路");
            if (list.Count() >= 7)
            {
                var info = new AchievementInfo
                {
                    Name = "七匹狼",
                    Description = "累计抽出7把狼的末路",
                    IsFinished = true,
                    FinishTime = list.ElementAt(6).Time,
                    Total = $"总计 {list.Count()}",
                };
                infos.Add(info);
            }

        }


        /// <summary>
        /// 在一次「神铸赋形」活动祈愿中抽出7把狼的末路，还没有抽出护摩之杖
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 七匹狼的诅咒(List<WishData> datas, List<AchievementInfo> infos)
        {
            var events = WishEventList.Where(x => x.UpStar5.Contains("护摩之杖"));
            foreach (var item in events)
            {
                var list = datas.Where(x => x.Time >= item.StartTime && x.Time <= item.EndTime).Where(x => x.Name == "狼的末路" || x.Name == "护摩之杖");
                int langmo = 0;
                DateTime time = DateTime.Now;
                foreach (var data in list)
                {
                    if (data.Name == "狼的末路")
                    {
                        langmo++;
                        time = data.Time;
                    }
                    else
                    {
                        break;
                    }
                }
                if (langmo >= 7)
                {
                    var info = new AchievementInfo
                    {
                        Name = "七匹狼的诅咒",
                        Description = "在一次「神铸赋形」活动祈愿中抽出7把狼的末路，还没有抽出护摩之杖",
                        IsFinished = true,
                        FinishTime = time,
                        Total = $"总计 {langmo}",
                    };
                    infos.Add(info);
                }
                if (langmo > 0)
                {
                    var info = new AchievementInfo
                    {
                        Name = "七匹狼的诅咒",
                        Description = "在一次「神铸赋形」活动祈愿中抽出7把狼的末路，还没有抽出护摩之杖",
                        IsFinished = false,
                        Progress = $"{langmo}/7",
                    };
                    infos.Add(info);
                }
            }

        }


        /// <summary>
        /// 在一次「浮生孰来」活动祈愿中抽中「甘雨」和「刻晴」
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="infos"></param>
        private static void 心甘晴愿(List<WishData> datas, List<AchievementInfo> infos)
        {
            var events = WishEventList.FindAll(x => x.Name == "浮生孰来");
            foreach (var item in events)
            {
                var list = datas.Where(x => x.Time >= item.StartTime && x.Time <= item.EndTime);
                list = list.Where(x => x.Name == "甘雨" || x.Name == "刻晴").Select(x => x);
                bool hasGanyu = false, hasKeqing = false;
                foreach (var data in list)
                {
                    if (data.Name == "甘雨")
                    {
                        hasGanyu = true;
                    }
                    else
                    {
                        hasKeqing = true;
                    }
                    if (hasGanyu && hasKeqing)
                    {
                        var info = new AchievementInfo
                        {
                            Name = "心甘晴愿",
                            Description = "在一次「浮生孰来」活动祈愿中抽中「甘雨」和「刻晴」",
                            IsFinished = true,
                            FinishTime = data.Time,
                        };
                        infos.Add(info);
                        break;
                    }
                }
            }
        }


        private static void 为什么不问问神奇的阿贝多呢(List<WishData> datas, List<AchievementInfo> infos)
        {
            //todo
            //var info = new AchievementInfo
            //{
            //    Name = "",
            //    Description = "",
            //    Comment = "",
            //    IsFinished = true,
            //    FinishTime = ,
            //    Total = $"总计 {}",
            //    Progress = $"{}/",
            //};
            //infos.Add(info);
        }








        #endregion


    }
}
