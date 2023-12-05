//本文件为自动生成，请不要手动修改
using System;
using System.Collections.Generic;
namespace ConfigAuto
{
	public partial class Config_Test
	{
		private static Config_Test _Test;
		public static Config_Test Test
		{
			get
			{
				if (_Test == null)
					_Test = new()
					{
						data = new()
						{
							testobj = new()
							{
								a = @"1",
								b = 2,
							},
							testint = new Dictionary<Int32, Int32>
							{
								{1, 2},
								{2, 3},
							},
							teststring = new Dictionary<Int32, String>
							{
								{1, @"2"},
								{2, @"3"},
							},
							testlist = new Dictionary<Int32, List<Int32>>
							{
								{1, 
									new()
									{
										1,2,3,
									}
								},
							},
							denglu = new Dictionary<Int32, denglu>
							{
								{1, new denglu {
									a = @"123",
								}},
								{2, new denglu {
									b = @"asdf",
								}},
								{3, new denglu {
									c = @"123",
									b = @"111",
								}},
							},
							testdenglulist = new Dictionary<Int32, List<testdenglulist>>
							{
								{1, 
									new()
									{

										new()
										{
											a = @"123",
										},
										new()
										{
											b = @"asdf",
										},
									}
								},
								{2, 
									new()
									{

										new()
										{
											c = @"123",
										},
										new()
										{
											b = @"asdf",
										},
									}
								},
							},
						},
					};
				return _Test;
			}
		}
		public Rootdata data {get;set;}
		public partial class Rootdata
		{
			public testobj testobj {get;set;}
			public Dictionary<Int32, Int32> testint {get;set;}
			public Dictionary<Int32, String> teststring {get;set;}
			public Dictionary<Int32, List<Int32>> testlist {get;set;}
			public Dictionary<Int32, denglu> denglu {get;set;}
			public Dictionary<Int32, List<testdenglulist>> testdenglulist {get;set;}
		}
		public partial class testobj
		{
			public String a {get;set;}
			public Int32 b {get;set;}
		}
		public partial class denglu
		{
			public String a {get;set;}
			public String b {get;set;}
			public String c {get;set;}
		}
		public partial class testdenglulist
		{
			public String a {get;set;}
			public String b {get;set;}
			public String c {get;set;}
		}

    }
}