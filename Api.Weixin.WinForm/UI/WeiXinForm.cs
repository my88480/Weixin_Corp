using Api.Weixin.Qy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Api.Weixin.WinForm.UI
{
    public partial class WeiXinForm : Form
    {
        //https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token=WAQdlGCZ7KeOvy-OTZ8vfMvUGsqXHmIehM-HAcR85PybLRvfqaXQsMdVd48tM6vT9bOEpMJApOsVNuX06B3IEn3pT-ruaj8vB9DVb0iRYdrfbg2k6W2dsVFCVf18A5_nSkBGaLwmj7ypI0-IuFw_ZxRaUGXI_6ioLz_pMXW-fCnsJDxqfDvpwh9xpDgkbzRzgfiHhk1WKWkb8SEzA-PrsA}
        string CorpID = "wwf5abcb0a838194dc";
        string CorpSecret = "OrmVJz2Hnex7PyIIc7y6s2x42jcnzEPRj64fCdkXXXX";
        int AgentID = 1000003;
        string Token;

        /// <summary>
        /// 基础URL
        /// </summary>
        const string baseUrl = "https://qyapi.weixin.qq.com/cgi-bin/";

        Api.Weixin.Qy.ApiHelper helper;

        public WeiXinForm()
        {
            InitializeComponent();

            helper = new ApiHelper();
        }

        /// <summary>
        /// 指定发送人发送消息
        /// 多人使用格式：wusanqiang|qianchangbin
        /// </summary>
        /// <param name="msg"></param>
        public void SendWXMsg(string person,string msg)
        {
            SendMessageRequest message;
            //string msgToken = "N3E-uFmrccFMZK7Rbfi2wzQQiQX6XKja79eii03XXXX";

            message = new SendMessageRequest();

            message.agentid = AgentID;

            message.msgtype = "text";
            message.touser = person;

            Core.Models.Text content = new Core.Models.Text();
            content.content = msg;

            message.text = content;

            SendMessageResult msgResult = helper.SendMessage(Token, message);

            Console.WriteLine("错误代码：" + msgResult.errcode);
            Console.WriteLine("错误消息：" + msgResult.errmsg);
            //MessageBox.Show("错误代码：" + msgResult.errcode + "\r\n" + "错误消息：" + msgResult.errmsg);
            //Console.WriteLine("错误消息：" + msgResult.errmsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void SendWXMsg(string msg)
        {
            string person = "wusanqiang";//wusanqiang|qianchangbin
            SendWXMsg(person,msg);
        }
            private void button1_Click(object sender, EventArgs e)
        {
            GetAccessTokenResult tokenResult = helper.GetAccessToken(CorpID, CorpSecret);

            MessageBox.Show(tokenResult.access_token);

            Token = tokenResult.access_token;

            Console.WriteLine("Access Token：" + Token);
        }

        /// <summary>
        /// 多条信息发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string persons, content;

            int n = 0;

            SendWXMsg("企业微信信息自动发送测试");
            SendWXMsg(System.DateTime.Now.ToString());

            n = Convert.ToInt32(textBox2.Text);
            persons = textBox1.Text;
            string ctext = richTextBox1.Text;

            for (int i = 0;i < n;i++)
            {
                content = "【来自数控班组】第" + (i + 1) + "条信息:" + ctext;
                SendWXMsg(persons, content);
            }

            SendWXMsg("测试结束。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetAgentResult agentResult;
            agentResult = helper.GetAgent(Token, AgentID);

            MessageBox.Show("Agent名称：" + agentResult.name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetAgentListResult agentList = Api.Weixin.Qy.ApiHelper.GetAgentList(Token);

            Console.WriteLine("Agent列表：");
            Console.WriteLine("Agent数目：" + agentList.agentlist.Count() );
            foreach (AgentInfo info in agentList.agentlist)
            {
                Console.WriteLine("------------------" );
                Console.WriteLine("ID：" + info.agentid);
                Console.WriteLine("Name：" + info.name);
                MessageBox.Show("ID：" + info.agentid + "\r\n" + "Name：" + info.name);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            GetDepartmentResult depList;
            depList = helper.GetDepartmentList(Token);

            Console.WriteLine(depList.department.Count());
            Console.WriteLine(depList.department.First().name);

            Console.WriteLine("输出全部部门：");

            foreach (Department dep in depList.department)
            {
                Console.WriteLine("部门ID：" + dep.id + "\t" + "部门名称：" + dep.name);
            }
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="deptId">获取的部门id </param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员 </param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        private void button7_Click(object sender, EventArgs e)
        {
            GetUserListResult userList;
            //递归获取所有用户
            userList = helper.GetUserList(Token, 1, true, 0);

            foreach (UserInfo user in userList.userlist)
            {
                Console.WriteLine("用户ID：" + user.userid + "\t" + "用户名称：" + user.name);

            }
        }
    }
}
