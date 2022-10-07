using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AccountVo : BaseVo
{
    /// <summary>
    /// 账户名
    /// </summary>
    public string Account;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password;

    /// <summary>
    /// 对应学员id
    /// </summary>
    public int userID;


    public static AccountVo Parse(SqliteDataReader dr)
    {
        AccountVo vo = new AccountVo();
        
        vo.ID = Convert.ToInt32(dr["ID"]);
        vo.Account = dr["Account"].ToString().Trim();
        vo.Password = dr["Password"].ToString().Trim();
        vo.userID = Convert.ToInt32(dr["UserID"]);
        DataManager.GetIns().accountDic[vo.ID] = vo;

        return vo;
    }

}

