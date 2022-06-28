/****** Object:  Table [dbo].[JobPositionMapping]    Script Date: 1/4/2019 10:14:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobPositionMapping](
	[JobPosition] [varchar](200) NULL,
	[Status] [varchar](50) NULL,
	[NewJobPosition] [varchar](200) NULL,
	[Company] [varchar](50) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_JobPositionMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[JobPositionMapping] ON 

INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm', N'1', N'Administration', N'MMKSI', 664, CAST(0x0000A9C9011824F2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM.', N'1', N'Administration', N'MMKSI', 665, CAST(0x0000A9C9011824FE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Svc', N'1', N'Administration', N'MMKSI', 666, CAST(0x0000A9C90118250A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'1', N'Administration', N'MMKSI', 667, CAST(0x0000A9C901182513 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ASS_MGR', N'1', N'Service Manager', N'MMKSI', 668, CAST(0x0000A9C90118251C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'1', N'CS Staff (Customer Satisfaction Staff)', N'MMKSI', 669, CAST(0x0000A9C901182525 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'1', N'Others', N'MMKSI', 670, CAST(0x0000A9C90118252F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'1', N'CS Staff (Customer Satisfaction Staff)', N'MMKSI', 671, CAST(0x0000A9C901182537 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Direktur After Sales Service', N'1', N'Others', N'MMKSI', 672, CAST(0x0000A9C901182540 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'1', N'Service Advisor', N'MMKSI', 673, CAST(0x0000A9C90118254A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS', N'1', N'Instructor', N'MMKSI', 674, CAST(0x0000A9C901182553 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Instruktur', N'1', N'Instructor', N'MMKSI', 675, CAST(0x0000A9C90118255B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'KEPALA BENGKEL', N'1', N'Workshop Head', N'MMKSI', 676, CAST(0x0000A9C901182564 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'1', N'Leader', N'MMKSI', 677, CAST(0x0000A9C90118256E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader', N'1', N'Leader', N'MMKSI', 678, CAST(0x0000A9C901182577 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader Body Repair', N'1', N'Leader', N'MMKSI', 679, CAST(0x0000A9C901182581 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader MWS', N'1', N'Leader', N'MMKSI', 680, CAST(0x0000A9C901182590 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Manager', N'1', N'Service Manager', N'MMKSI', 681, CAST(0x0000A9C901182599 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'1', N'Mechanic', N'MMKSI', 682, CAST(0x0000A9C9011825A3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik', N'1', N'Mechanic', N'MMKSI', 683, CAST(0x0000A9C9011825AC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'1', N'Mechanic', N'MMKSI', 684, CAST(0x0000A9C9011825B6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'1', N'Oil Man', N'MMKSI', 685, CAST(0x0000A9C9011825BE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'1', N'Service Manager', N'MMKSI', 686, CAST(0x0000A9C9011825C9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR_C', N'1', N'Service Manager', N'MMKSI', 687, CAST(0x0000A9C9011825D3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF', N'1', N'Workshop Head', N'MMKSI', 688, CAST(0x0000A9C9011825DD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'1', N'Workshop Head', N'MMKSI', 689, CAST(0x0000A9C9011825E6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Sect', N'1', N'Others', N'MMKSI', 690, CAST(0x0000A9C9011825EF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADV & PROMOTION DEPT.', N'1', N'Others', N'MMKSI', 691, CAST(0x0000A9C9011825F8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Advertising & Promotion Dept.', N'1', N'Others', N'MMKSI', 692, CAST(0x0000A9C901182602 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Assistant Foreman (MKM)', N'1', N'Others', N'MMKSI', 693, CAST(0x0000A9C90118260B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CRD', N'1', N'Others', N'MMKSI', 694, CAST(0x0000A9C901182618 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS Fuso', N'1', N'Others', N'MMKSI', 695, CAST(0x0000A9C901182623 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CV', N'1', N'Others', N'MMKSI', 696, CAST(0x0000A9C90118262D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'DSD', N'1', N'Others', N'MMKSI', 697, CAST(0x0000A9C901182638 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Export Import Dept.', N'1', N'Others', N'MMKSI', 698, CAST(0x0000A9C901182642 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Factory Worker (MKM)', N'1', N'Others', N'MMKSI', 699, CAST(0x0000A9C90118264B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'1', N'Final Checker', N'MMKSI', 700, CAST(0x0000A9C901182654 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'1', N'Service Advisor', N'MMKSI', 701, CAST(0x0000A9C90118265D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Import & Export', N'1', N'Others', N'MMKSI', 702, CAST(0x0000A9C901182666 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Import & Export Dept.', N'1', N'Others', N'MMKSI', 703, CAST(0x0000A9C90118266F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LC', N'1', N'Leader', N'MMKSI', 704, CAST(0x0000A9C901182678 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LCV Dept', N'1', N'Others', N'MMKSI', 705, CAST(0x0000A9C901182684 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LCV Dept.', N'1', N'Others', N'MMKSI', 706, CAST(0x0000A9C901182690 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'1', N'Others', N'MMKSI', 707, CAST(0x0000A9C901182699 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Other', N'1', N'Others', N'MMKSI', 708, CAST(0x0000A9C9011826A2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Others', N'1', N'Others', N'MMKSI', 709, CAST(0x0000A9C9011826AE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Logistic Dept.', N'1', N'Others', N'MMKSI', 710, CAST(0x0000A9C9011826B8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Parts Logistic Dept', N'1', N'Others', N'MMKSI', 711, CAST(0x0000A9C9011826C1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Parts Logistic Dept.', N'1', N'Others', N'MMKSI', 712, CAST(0x0000A9C9011826CA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PC LCV', N'1', N'Others', N'MMKSI', 713, CAST(0x0000A9C9011826D4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PC STRATEGY DEPT.', N'1', N'Others', N'MMKSI', 714, CAST(0x0000A9C9011826DE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PCD', N'1', N'Others', N'MMKSI', 715, CAST(0x0000A9C9011826E6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PR Office', N'1', N'Others', N'MMKSI', 716, CAST(0x0000A9C9011826EF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Procurement Dept.', N'1', N'Others', N'MMKSI', 717, CAST(0x0000A9C9011826F8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Production Control Dept.', N'1', N'Others', N'MMKSI', 718, CAST(0x0000A9C901182701 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Q. Assurance', N'1', N'Others', N'MMKSI', 719, CAST(0x0000A9C90118270A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Quality Assurance', N'1', N'Others', N'MMKSI', 720, CAST(0x0000A9C901182714 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Quality Control', N'1', N'Others', N'MMKSI', 721, CAST(0x0000A9C90118271D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Rental Marketing Dept.', N'1', N'Others', N'MMKSI', 722, CAST(0x0000A9C901182726 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Retail Sales Development Dept.', N'1', N'Others', N'MMKSI', 723, CAST(0x0000A9C901182730 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Roadman', N'1', N'Others', N'MMKSI', 724, CAST(0x0000A9C901182738 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'RSD', N'1', N'Others', N'MMKSI', 725, CAST(0x0000A9C901182741 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Planning', N'1', N'Others', N'MMKSI', 726, CAST(0x0000A9C90118274A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Planning Dept.', N'1', N'Others', N'MMKSI', 727, CAST(0x0000A9C901182753 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Processing Dept.', N'1', N'Others', N'MMKSI', 728, CAST(0x0000A9C90118275B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Section Chief', N'1', N'Others', N'MMKSI', 729, CAST(0x0000A9C901182765 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'section head - acct', N'1', N'Others', N'MMKSI', 730, CAST(0x0000A9C90118276D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_A_SPV', N'1', N'', N'MMKSI', 731, CAST(0x0000A9C901182777 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_ADM', N'1', N'', N'MMKSI', 732, CAST(0x0000A9C901182782 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CLT', N'1', N'', N'MMKSI', 733, CAST(0x0000A9C90118278A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CNT', N'1', N'', N'MMKSI', 734, CAST(0x0000A9C901182795 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CSH', N'1', N'', N'MMKSI', 735, CAST(0x0000A9C90118279F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_MGR', N'1', N'', N'MMKSI', 736, CAST(0x0000A9C9011827AA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_RNR', N'1', N'', N'MMKSI', 737, CAST(0x0000A9C9011827B4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_S_SPV', N'1', N'', N'MMKSI', 738, CAST(0x0000A9C9011827C1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SF', N'1', N'', N'MMKSI', 739, CAST(0x0000A9C9011827D0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SLM', N'1', N'', N'MMKSI', 740, CAST(0x0000A9C9011827E3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SPV', N'1', N'', N'MMKSI', 741, CAST(0x0000A9C9011827F6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_W_SPV', N'1', N'', N'MMKSI', 742, CAST(0x0000A9C9011827FF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_WRH', N'1', N'', N'MMKSI', 743, CAST(0x0000A9C90118280A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff', N'1', N'Others', N'MMKSI', 744, CAST(0x0000A9C901182816 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff (ADV & PROMOTION DEPT.)', N'1', N'Others', N'MMKSI', 745, CAST(0x0000A9C901182821 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'STAFF (MARKET ANALYSIS DEPT.)', N'1', N'Others', N'MMKSI', 746, CAST(0x0000A9C90118282C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - acct', N'1', N'Others', N'MMKSI', 747, CAST(0x0000A9C901182838 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - GA Dept.', N'1', N'Others', N'MMKSI', 748, CAST(0x0000A9C901182845 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - legal dept', N'1', N'Others', N'MMKSI', 749, CAST(0x0000A9C901182852 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - QC', N'1', N'Others', N'MMKSI', 750, CAST(0x0000A9C90118285E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - RSD', N'1', N'Others', N'MMKSI', 751, CAST(0x0000A9C90118286B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - Sales Processing Dept.', N'1', N'Others', N'MMKSI', 752, CAST(0x0000A9C901182874 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff -GA', N'1', N'Others', N'MMKSI', 753, CAST(0x0000A9C90118287E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff -RSD', N'1', N'Others', N'MMKSI', 754, CAST(0x0000A9C901182887 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff QC', N'1', N'Others', N'MMKSI', 755, CAST(0x0000A9C901182890 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff RSD', N'1', N'Others', N'MMKSI', 756, CAST(0x0000A9C901182899 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff WSC', N'1', N'Others', N'MMKSI', 757, CAST(0x0000A9C9011828A2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff- GA', N'1', N'Others', N'MMKSI', 758, CAST(0x0000A9C9011828AB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff-PRD', N'1', N'Others', N'MMKSI', 759, CAST(0x0000A9C9011828B5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Strada Triton Office', N'1', N'Others', N'MMKSI', 760, CAST(0x0000A9C9011828C0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC', N'1', N'Others', N'MMKSI', 761, CAST(0x0000A9C9011828CA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Technical', N'1', N'Others', N'MMKSI', 762, CAST(0x0000A9C9011828D3 AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Technical Dept.', N'1', N'Others', N'MMKSI', 763, CAST(0x0000A9C9011828DC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'1', N'Others', N'MMKSI', 764, CAST(0x0000A9C9011828E4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Training Dept.', N'1', N'Others', N'MMKSI', 765, CAST(0x0000A9C9011828ED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Training Support', N'1', N'Others', N'MMKSI', 766, CAST(0x0000A9C9011828F7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRUCK & BUS Dept.', N'1', N'Others', N'MMKSI', 767, CAST(0x0000A9C9011828FF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRUCK&BUS', N'1', N'Others', N'MMKSI', 768, CAST(0x0000A9C901182907 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VC Dept CBU Import', N'1', N'Others', N'MMKSI', 769, CAST(0x0000A9C901182910 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VC Dept.', N'1', N'Others', N'MMKSI', 770, CAST(0x0000A9C901182919 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Vehicle Control', N'1', N'Others', N'MMKSI', 771, CAST(0x0000A9C901182921 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM', N'2', N'Administration', N'MMKSI', 772, CAST(0x0000A9C90118292A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Service', N'2', N'Administration', N'MMKSI', 773, CAST(0x0000A9C901182933 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Svc', N'2', N'Administration', N'MMKSI', 774, CAST(0x0000A9C90118293C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm. Service', N'2', N'Administration', N'MMKSI', 775, CAST(0x0000A9C901182945 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Section', N'2', N'Administration', N'MMKSI', 776, CAST(0x0000A9C901182950 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM.SERVICE', N'2', N'Administration', N'MMKSI', 777, CAST(0x0000A9C90118295A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Svc', N'2', N'Administration', N'MMKSI', 778, CAST(0x0000A9C901182964 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SPR', N'2', N'', N'MMKSI', 779, CAST(0x0000A9C90118296D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'2', N'Administration', N'MMKSI', 780, CAST(0x0000A9C901182977 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmHead', N'2', N'Administration', N'MMKSI', 781, CAST(0x0000A9C901182983 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADMINISTRASI', N'2', N'Administration', N'MMKSI', 782, CAST(0x0000A9C90118298D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'administrasi, front girl', N'2', N'Administration', N'MMKSI', 783, CAST(0x0000A9C901182999 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Administration Service', N'2', N'Administration', N'MMKSI', 784, CAST(0x0000A9C9011829C3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADMINSTRASI SERVICE', N'2', N'Administration', N'MMKSI', 785, CAST(0x0000A9C9011829D4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmService', N'2', N'Administration', N'MMKSI', 786, CAST(0x0000A9C9011829E5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmSvc', N'2', N'Administration', N'MMKSI', 787, CAST(0x0000A9C9011829F0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Asisten Service Manager', N'2', N'Others', N'MMKSI', 788, CAST(0x0000A9C901182A1A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ass mekanik', N'2', N'Others', N'MMKSI', 789, CAST(0x0000A9C901182A27 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Foreman QA', N'2', N'Others', N'MMKSI', 790, CAST(0x0000A9C901182A30 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Manager', N'2', N'Others', N'MMKSI', 791, CAST(0x0000A9C901182A3A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Mgr', N'2', N'Others', N'MMKSI', 792, CAST(0x0000A9C901182A43 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AssMekanik', N'2', N'Others', N'MMKSI', 793, CAST(0x0000A9C901182A4C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'BGKL', N'2', N'Others', N'MMKSI', 794, CAST(0x0000A9C901182A54 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair', N'2', N'Others', N'MMKSI', 795, CAST(0x0000A9C901182A5D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair Head', N'2', N'Others', N'MMKSI', 796, CAST(0x0000A9C901182A66 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair leader', N'2', N'Others', N'MMKSI', 797, CAST(0x0000A9C901182A6E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair mech', N'2', N'Others', N'MMKSI', 798, CAST(0x0000A9C901182A77 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'BodyRepair', N'2', N'Others', N'MMKSI', 799, CAST(0x0000A9C901182A81 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'C.ustomer service', N'2', N'Others', N'MMKSI', 800, CAST(0x0000A9C901182A8A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Chief', N'2', N'Others', N'MMKSI', 801, CAST(0x0000A9C901182A93 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Chief W/S', N'2', N'Others', N'MMKSI', 802, CAST(0x0000A9C901182A9C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Cianjur', N'2', N'Others', N'MMKSI', 803, CAST(0x0000A9C901182AA6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Coordinator', N'2', N'Others', N'MMKSI', 804, CAST(0x0000A9C901182AAF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Coordinator LBU Group', N'2', N'Others', N'MMKSI', 805, CAST(0x0000A9C901182AB8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CRD', N'2', N'Others', N'MMKSI', 806, CAST(0x0000A9C901182AC1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'2', N'Others', N'MMKSI', 807, CAST(0x0000A9C901182ACB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS dan Front Lady', N'2', N'Others', N'MMKSI', 808, CAST(0x0000A9C901182AD3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS Fuso', N'2', N'Others', N'MMKSI', 809, CAST(0x0000A9C901182ADC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'2', N'Others', N'MMKSI', 810, CAST(0x0000A9C901182AEE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'2', N'Others', N'MMKSI', 811, CAST(0x0000A9C901182AF7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CUST.SERVICE', N'2', N'Others', N'MMKSI', 812, CAST(0x0000A9C901182B00 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Follow', N'2', N'Others', N'MMKSI', 813, CAST(0x0000A9C901182B0A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Service', N'2', N'Others', N'MMKSI', 814, CAST(0x0000A9C901182B13 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Service Sales Dept.', N'2', N'Others', N'MMKSI', 815, CAST(0x0000A9C901182B1C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Svc', N'2', N'Others', N'MMKSI', 816, CAST(0x0000A9C901182B25 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CV Sect', N'2', N'Others', N'MMKSI', 817, CAST(0x0000A9C901182B2E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Driver', N'2', N'Others', N'MMKSI', 818, CAST(0x0000A9C901182B36 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F. Checker', N'2', N'Others', N'MMKSI', 819, CAST(0x0000A9C901182B3F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Checker', N'2', N'Others', N'MMKSI', 820, CAST(0x0000A9C901182B48 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Checker & Bodyrepair', N'2', N'Others', N'MMKSI', 821, CAST(0x0000A9C901182B50 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Head', N'2', N'Others', N'MMKSI', 822, CAST(0x0000A9C901182B59 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'2', N'Others', N'MMKSI', 823, CAST(0x0000A9C901182B62 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FCC', N'2', N'Others', N'MMKSI', 824, CAST(0x0000A9C901182B6A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Final Checker', N'2', N'Others', N'MMKSI', 825, CAST(0x0000A9C901182B73 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Final Checker Body Repair', N'2', N'Others', N'MMKSI', 826, CAST(0x0000A9C901182B7C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FinalCheck', N'2', N'Others', N'MMKSI', 827, CAST(0x0000A9C901182B84 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'2', N'Others', N'MMKSI', 828, CAST(0x0000A9C901182B8C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'2', N'Others', N'MMKSI', 829, CAST(0x0000A9C901182B95 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'front girl', N'2', N'Others', N'MMKSI', 830, CAST(0x0000A9C901182B9D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Head', N'2', N'Others', N'MMKSI', 831, CAST(0x0000A9C901182BA6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT LADIES', N'2', N'Others', N'MMKSI', 832, CAST(0x0000A9C901182BAE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Lady', N'2', N'Others', N'MMKSI', 833, CAST(0x0000A9C901182BB6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT LINE', N'2', N'Others', N'MMKSI', 834, CAST(0x0000A9C901182BBE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Liner', N'2', N'Others', N'MMKSI', 835, CAST(0x0000A9C901182BC7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Man', N'2', N'Others', N'MMKSI', 836, CAST(0x0000A9C901182BD0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT OFFICE', N'2', N'Others', N'MMKSI', 837, CAST(0x0000A9C901182BD9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontgirl/CS', N'2', N'Others', N'MMKSI', 838, CAST(0x0000A9C901182BE2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontlady', N'2', N'Others', N'MMKSI', 839, CAST(0x0000A9C901182BEA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontman', N'2', N'Others', N'MMKSI', 840, CAST(0x0000A9C901182BF3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'GM', N'2', N'Others', N'MMKSI', 841, CAST(0x0000A9C901182BFC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Helper', N'2', N'Others', N'MMKSI', 842, CAST(0x0000A9C901182C04 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'HLP', N'2', N'Others', N'MMKSI', 843, CAST(0x0000A9C901182C0D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'HRD ', N'2', N'Others', N'MMKSI', 844, CAST(0x0000A9C901182C16 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS', N'2', N'Others', N'MMKSI', 845, CAST(0x0000A9C901182C1E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INSTRUCTURE', N'2', N'Others', N'MMKSI', 846, CAST(0x0000A9C901182C28 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'IS', N'2', N'Others', N'MMKSI', 847, CAST(0x0000A9C901182C30 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ka. Bengkel', N'2', N'Others', N'MMKSI', 848, CAST(0x0000A9C901182C39 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ka. Service', N'2', N'Others', N'MMKSI', 849, CAST(0x0000A9C901182C42 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'KaCab', N'2', N'Others', N'MMKSI', 850, CAST(0x0000A9C901182C4B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'kader Mekanik', N'2', N'Others', N'MMKSI', 851, CAST(0x0000A9C901182C54 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Kasie. Tools & Equipment', N'2', N'Others', N'MMKSI', 852, CAST(0x0000A9C901182C5C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'kasir', N'2', N'Others', N'MMKSI', 853, CAST(0x0000A9C901182C64 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Kepala Bengkel', N'2', N'Others', N'MMKSI', 854, CAST(0x0000A9C901182C6D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Koordinator daerah', N'2', N'Others', N'MMKSI', 855, CAST(0x0000A9C901182C75 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LC', N'2', N'Others', N'MMKSI', 856, CAST(0x0000A9C901182C7E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'2', N'Others', N'MMKSI', 857, CAST(0x0000A9C901182C86 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'leader', N'2', N'Others', N'MMKSI', 858, CAST(0x0000A9C901182C8F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader Mekanik', N'2', N'Others', N'MMKSI', 859, CAST(0x0000A9C901182C97 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader MWS', N'2', N'Others', N'MMKSI', 860, CAST(0x0000A9C901182C9F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Lubrication', N'2', N'Others', N'MMKSI', 861, CAST(0x0000A9C901182CA8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'M', N'2', N'Others', N'MMKSI', 862, CAST(0x0000A9C901182CB0 AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Maintenance', N'2', N'Others', N'MMKSI', 863, CAST(0x0000A9C901182CB9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Management Trainee', N'2', N'Others', N'MMKSI', 864, CAST(0x0000A9C901182CC1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Manager', N'2', N'Others', N'MMKSI', 865, CAST(0x0000A9C901182CC9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'manajer', N'2', N'Others', N'MMKSI', 866, CAST(0x0000A9C901182CD2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MARKETING', N'2', N'Others', N'MMKSI', 867, CAST(0x0000A9C901182CDA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MARKETING SERVICE', N'2', N'Others', N'MMKSI', 868, CAST(0x0000A9C901182CE3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Marketing Service/CS Team', N'2', N'Others', N'MMKSI', 869, CAST(0x0000A9C901182CEB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MarkSvc', N'2', N'Others', N'MMKSI', 870, CAST(0x0000A9C901182CF3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Material Controlling Mks', N'2', N'Others', N'MMKSI', 871, CAST(0x0000A9C901182CFC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'2', N'Others', N'MMKSI', 872, CAST(0x0000A9C901182D04 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekaniik', N'2', N'Others', N'MMKSI', 873, CAST(0x0000A9C901182D0C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik', N'2', N'Others', N'MMKSI', 874, CAST(0x0000A9C901182D14 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK BODY REPAIR', N'2', N'Others', N'MMKSI', 875, CAST(0x0000A9C901182D1D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Body Repair Dept.', N'2', N'Others', N'MMKSI', 876, CAST(0x0000A9C901182D25 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik cab Duri', N'2', N'Others', N'MMKSI', 877, CAST(0x0000A9C901182D2E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK Cabang Simpang', N'2', N'Others', N'MMKSI', 878, CAST(0x0000A9C901182D37 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK HELPER', N'2', N'Others', N'MMKSI', 879, CAST(0x0000A9C901182D3F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik J', N'2', N'Others', N'MMKSI', 880, CAST(0x0000A9C901182D47 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Junior', N'2', N'Others', N'MMKSI', 881, CAST(0x0000A9C901182D4F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik MTB 2', N'2', N'Others', N'MMKSI', 882, CAST(0x0000A9C901182D58 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK MWS', N'2', N'Others', N'MMKSI', 883, CAST(0x0000A9C901182D60 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik OJT', N'2', N'Others', N'MMKSI', 884, CAST(0x0000A9C901182D68 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik PDC', N'2', N'Others', N'MMKSI', 885, CAST(0x0000A9C901182D71 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Senior', N'2', N'Others', N'MMKSI', 886, CAST(0x0000A9C901182D79 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Trainee', N'2', N'Others', N'MMKSI', 887, CAST(0x0000A9C901182D82 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Yunior', N'2', N'Others', N'MMKSI', 888, CAST(0x0000A9C901182D8A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'2', N'Others', N'MMKSI', 889, CAST(0x0000A9C901182D93 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MT', N'2', N'Others', N'MMKSI', 890, CAST(0x0000A9C901182D9C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MW', N'2', N'Others', N'MMKSI', 891, CAST(0x0000A9C901182DA4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Office Boy', N'2', N'Others', N'MMKSI', 892, CAST(0x0000A9C901182DAD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Oil Man', N'2', N'Others', N'MMKSI', 893, CAST(0x0000A9C901182DB6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OilLeader', N'2', N'Others', N'MMKSI', 894, CAST(0x0000A9C901182DBE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Oilman', N'2', N'Others', N'MMKSI', 895, CAST(0x0000A9C901182DC8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'2', N'Others', N'MMKSI', 896, CAST(0x0000A9C901182DD1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'2', N'Others', N'MMKSI', 897, CAST(0x0000A9C901182DDB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Other', N'2', N'Others', N'MMKSI', 898, CAST(0x0000A9C901182DE4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Others', N'2', N'Others', N'MMKSI', 899, CAST(0x0000A9C901182DED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'P_Mekanik', N'2', N'Others', N'MMKSI', 900, CAST(0x0000A9C901182DF6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PART', N'2', N'Others', N'MMKSI', 901, CAST(0x0000A9C901182DFF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Counter', N'2', N'Others', N'MMKSI', 902, CAST(0x0000A9C901182E09 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Runner', N'2', N'Others', N'MMKSI', 903, CAST(0x0000A9C901182E11 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Runner Svc', N'2', N'Others', N'MMKSI', 904, CAST(0x0000A9C901182E1B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'partruner', N'2', N'Others', N'MMKSI', 905, CAST(0x0000A9C901182E24 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Partrunner', N'2', N'Others', N'MMKSI', 906, CAST(0x0000A9C901182E2D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'partsrunner', N'2', N'Others', N'MMKSI', 907, CAST(0x0000A9C901182E37 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Pembantu Mekanik', N'2', N'Others', N'MMKSI', 908, CAST(0x0000A9C901182E40 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PembMekanik', N'2', N'Others', N'MMKSI', 909, CAST(0x0000A9C901182E49 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Penanggung Jawab Service', N'2', N'Others', N'MMKSI', 910, CAST(0x0000A9C901182E52 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Penj. Service', N'2', N'Others', N'MMKSI', 911, CAST(0x0000A9C901182E5A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PENSIUN DINI', N'2', N'Others', N'MMKSI', 912, CAST(0x0000A9C901182E63 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Pjs. LEADER', N'2', N'Others', N'MMKSI', 913, CAST(0x0000A9C901182E6C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PQR/WSC & CS TEAM', N'2', N'Others', N'MMKSI', 914, CAST(0x0000A9C901182E74 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PR', N'2', N'Others', N'MMKSI', 915, CAST(0x0000A9C901182E7D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'QC', N'2', N'Others', N'MMKSI', 916, CAST(0x0000A9C901182E86 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'RSD', N'2', N'Others', N'MMKSI', 917, CAST(0x0000A9C901182E8E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'S/P', N'2', N'Others', N'MMKSI', 918, CAST(0x0000A9C901182E97 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SALES', N'2', N'Others', N'MMKSI', 919, CAST(0x0000A9C901182EA0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Department', N'2', N'Others', N'MMKSI', 920, CAST(0x0000A9C901182EAA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Supervisor', N'2', N'Others', N'MMKSI', 921, CAST(0x0000A9C901182EB6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SBM', N'2', N'Others
', N'MMKSI', 922, CAST(0x0000A9C901182EC2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SC', N'2', N'Others', N'MMKSI', 923, CAST(0x0000A9C901182ED1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SCN', N'2', N'Others', N'MMKSI', 924, CAST(0x0000A9C901182EDD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'service advicer', N'2', N'Others', N'MMKSI', 925, CAST(0x0000A9C901182EE8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service adviser', N'2', N'Others', N'MMKSI', 926, CAST(0x0000A9C901182EF5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SERVICE ADVISOR', N'2', N'Others', N'MMKSI', 927, CAST(0x0000A9C901182F14 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Advisor 01', N'2', N'Others', N'MMKSI', 928, CAST(0x0000A9C901182F21 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SERVICE DEPT.', N'2', N'Others', N'MMKSI', 929, CAST(0x0000A9C901182F2D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Head', N'2', N'Others', N'MMKSI', 930, CAST(0x0000A9C901182F3A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Manager', N'2', N'Others', N'MMKSI', 931, CAST(0x0000A9C901182F44 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SL', N'2', N'Others', N'MMKSI', 932, CAST(0x0000A9C901182F4C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SP', N'2', N'Others', N'MMKSI', 933, CAST(0x0000A9C901182F56 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare part', N'2', N'Others', N'MMKSI', 934, CAST(0x0000A9C901182F62 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare part adm', N'2', N'Others', N'MMKSI', 935, CAST(0x0000A9C901182F6B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Spare Part Department', N'2', N'Others', N'MMKSI', 936, CAST(0x0000A9C901182F74 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare Parts', N'2', N'Others', N'MMKSI', 937, CAST(0x0000A9C901182F7E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Spare Parts Gudang', N'2', N'Others', N'MMKSI', 938, CAST(0x0000A9C901182F87 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPAREPART', N'2', N'Others', N'MMKSI', 939, CAST(0x0000A9C901182F90 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SpareParts', N'2', N'Others', N'MMKSI', 940, CAST(0x0000A9C901182F98 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_A_SPV', N'2', N'', N'MMKSI', 941, CAST(0x0000A9C901182FA1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_ADM', N'2', N'', N'MMKSI', 942, CAST(0x0000A9C901182FA9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CLT', N'2', N'', N'MMKSI', 943, CAST(0x0000A9C901182FB4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CNT', N'2', N'', N'MMKSI', 944, CAST(0x0000A9C901182FBD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CSH', N'2', N'', N'MMKSI', 945, CAST(0x0000A9C901182FC5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_MGR', N'2', N'', N'MMKSI', 946, CAST(0x0000A9C901182FCD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_RNR', N'2', N'', N'MMKSI', 947, CAST(0x0000A9C901182FD5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_S_SPV', N'2', N'', N'MMKSI', 948, CAST(0x0000A9C901182FDE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SF', N'2', N'', N'MMKSI', 949, CAST(0x0000A9C901182FE6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SLM', N'2', N'', N'MMKSI', 950, CAST(0x0000A9C901182FEF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SPV', N'2', N'', N'MMKSI', 951, CAST(0x0000A9C901182FF7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_W_SPV', N'2', N'', N'MMKSI', 952, CAST(0x0000A9C901182FFF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_WRH', N'2', N'', N'MMKSI', 953, CAST(0x0000A9C90118300C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPV', N'2', N'Others', N'MMKSI', 954, CAST(0x0000A9C901183015 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff', N'2', N'Others', N'MMKSI', 955, CAST(0x0000A9C90118301D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'STAFF BODY REPAIR', N'2', N'Others', N'MMKSI', 956, CAST(0x0000A9C901183026 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff Penagihan Service', N'2', N'Others', N'MMKSI', 957, CAST(0x0000A9C90118302E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SUPERVISOR', N'2', N'Others', N'MMKSI', 958, CAST(0x0000A9C901183036 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'svc adv', N'2', N'Others', N'MMKSI', 959, CAST(0x0000A9C90118303F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Adv.', N'2', N'Others', N'MMKSI', 960, CAST(0x0000A9C901183048 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC Coordination', N'2', N'Others', N'MMKSI', 961, CAST(0x0000A9C901183052 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC COORDINATOOR', N'2', N'Others', N'MMKSI', 962, CAST(0x0000A9C90118305B AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Head', N'2', N'Others', N'MMKSI', 963, CAST(0x0000A9C901183063 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Koordinator Jatim & Bali', N'2', N'Others', N'MMKSI', 964, CAST(0x0000A9C90118306D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Mgr', N'2', N'Others', N'MMKSI', 965, CAST(0x0000A9C901183083 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC SPV', N'2', N'Others', N'MMKSI', 966, CAST(0x0000A9C90118308D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc. Head', N'2', N'Others', N'MMKSI', 967, CAST(0x0000A9C901183095 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC. Koordinator', N'2', N'Others', N'MMKSI', 968, CAST(0x0000A9C90118309E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_HEAD', N'2', N'Others', N'MMKSI', 969, CAST(0x0000A9C9011830A7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'2', N'Others', N'MMKSI', 970, CAST(0x0000A9C9011830B0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR_C', N'2', N'Others', N'MMKSI', 971, CAST(0x0000A9C9011830B9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_S', N'2', N'Others', N'MMKSI', 972, CAST(0x0000A9C9011830C2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'technical supervisor', N'2', N'Others', N'MMKSI', 973, CAST(0x0000A9C9011830CB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TK', N'2', N'Others', N'MMKSI', 974, CAST(0x0000A9C9011830D3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'2', N'Others', N'MMKSI', 975, CAST(0x0000A9C9011830DC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TOOL MAN', N'2', N'Others', N'MMKSI', 976, CAST(0x0000A9C9011830E4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolkeep', N'2', N'Others', N'MMKSI', 977, CAST(0x0000A9C9011830ED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolkeep.', N'2', N'Others', N'MMKSI', 978, CAST(0x0000A9C9011830F5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Tools Keeper', N'2', N'Others', N'MMKSI', 979, CAST(0x0000A9C9011830FE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Tools Man', N'2', N'Others', N'MMKSI', 980, CAST(0x0000A9C901183107 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TOOLS ROOM', N'2', N'Others', N'MMKSI', 981, CAST(0x0000A9C901183110 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolskeep', N'2', N'Others', N'MMKSI', 982, CAST(0x0000A9C901183119 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolsman', N'2', N'Others', N'MMKSI', 983, CAST(0x0000A9C901183121 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolsman & Maintenance', N'2', N'Others', N'MMKSI', 984, CAST(0x0000A9C90118312A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRAINER', N'2', N'Others', N'MMKSI', 985, CAST(0x0000A9C901183133 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRAINING CHIEF', N'2', N'Others', N'MMKSI', 986, CAST(0x0000A9C90118313B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VR', N'2', N'Others', N'MMKSI', 987, CAST(0x0000A9C901183143 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'W/s Chief', N'2', N'Others', N'MMKSI', 988, CAST(0x0000A9C90118314C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WORKSHOP CHIEF', N'2', N'Others', N'MMKSI', 989, CAST(0x0000A9C901183155 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WORKSHOP HEAD', N'2', N'Others', N'MMKSI', 990, CAST(0x0000A9C90118315D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Workshop Sect.Head', N'2', N'Others', N'MMKSI', 991, CAST(0x0000A9C901183165 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF', N'2', N'Others', N'MMKSI', 992, CAST(0x0000A9C90118316D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'2', N'Others', N'MMKSI', 993, CAST(0x0000A9C901183176 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WS Chief', N'2', N'Others', N'MMKSI', 994, CAST(0x0000A9C90118317E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WSC-TRAINING SECTION', N'2', N'Others', N'MMKSI', 995, CAST(0x0000A9C901183187 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SPR', N'0', N'', N'MMKSI', 996, CAST(0x0000A9C90118318F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'0', N'Administration', N'MMKSI', 997, CAST(0x0000A9C901183197 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ASS_MGR', N'0', N'Service Manager', N'MMKSI', 998, CAST(0x0000A9C9011831A0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CRD', N'0', N'Others', N'MMKSI', 999, CAST(0x0000A9C9011831A9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'0', N'CS Staff (Customer Satisfaction Staff)', N'MMKSI', 1000, CAST(0x0000A9C9011831B1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'0', N'Others', N'MMKSI', 1001, CAST(0x0000A9C9011831BA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'0', N'CS Staff (Customer Satisfaction Staff)', N'MMKSI', 1002, CAST(0x0000A9C9011831C3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'0', N'Final Checker', N'MMKSI', 1003, CAST(0x0000A9C9011831CC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'0', N'Service Advisor', N'MMKSI', 1004, CAST(0x0000A9C9011831D5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'0', N'Service Advisor', N'MMKSI', 1005, CAST(0x0000A9C9011831DD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'0', N'Leader', N'MMKSI', 1006, CAST(0x0000A9C9011831E6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'0', N'Mechanic', N'MMKSI', 1007, CAST(0x0000A9C9011831EF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'0', N'Mechanic', N'MMKSI', 1008, CAST(0x0000A9C9011831F7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'0', N'Oil Man', N'MMKSI', 1009, CAST(0x0000A9C901183200 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'0', N'Others', N'MMKSI', 1010, CAST(0x0000A9C901183209 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SBM', N'0', N'', N'MMKSI', 1011, CAST(0x0000A9C901183211 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_ADM', N'0', N'', N'MMKSI', 1012, CAST(0x0000A9C90118321A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_CNT', N'0', N'', N'MMKSI', 1013, CAST(0x0000A9C901183222 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_Cntr', N'0', N'', N'MMKSI', 1014, CAST(0x0000A9C90118322A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_MGR', N'0', N'', N'MMKSI', 1015, CAST(0x0000A9C901183233 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_RNR', N'0', N'', N'MMKSI', 1016, CAST(0x0000A9C90118323C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SLM', N'0', N'', N'MMKSI', 1017, CAST(0x0000A9C901183244 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_SPV', N'0', N'', N'MMKSI', 1018, CAST(0x0000A9C90118324C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_WRH', N'0', N'', N'MMKSI', 1019, CAST(0x0000A9C901183255 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'0', N'Service Manager', N'MMKSI', 1020, CAST(0x0000A9C90118325D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'0', N'Tools Man', N'MMKSI', 1021, CAST(0x0000A9C901183266 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF', N'0', N'Workshop Head', N'MMKSI', 1022, CAST(0x0000A9C90118326F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'0', N'Workshop Head', N'MMKSI', 1023, CAST(0x0000A9C901183278 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm', N'1', N'Administration', N'KTB', 1024, CAST(0x0000A9C90118E880 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM', N'2', N'Administration', N'KTB', 1025, CAST(0x0000A9C90118E88B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Sect', N'1', N'Administration', N'KTB', 1026, CAST(0x0000A9C90118E894 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Service', N'2', N'Administration', N'KTB', 1027, CAST(0x0000A9C90118E89E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm Svc', N'2', N'Administration', N'KTB', 1028, CAST(0x0000A9C90118E8A8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM.', N'1', N'Administration', N'KTB', 1029, CAST(0x0000A9C90118E8B1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm. Service', N'2', N'Administration', N'KTB', 1030, CAST(0x0000A9C90118E8BA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Section', N'2', N'Administration', N'KTB', 1031, CAST(0x0000A9C90118E8C3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM.SERVICE', N'2', N'Administration', N'KTB', 1032, CAST(0x0000A9C90118E8CB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Svc', N'1', N'Administration', N'KTB', 1033, CAST(0x0000A9C90118E8D4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Adm.Svc', N'2', N'Administration', N'KTB', 1034, CAST(0x0000A9C90118E8DD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'2', N'Administration', N'KTB', 1035, CAST(0x0000A9C90118E8E8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'1', N'Administration', N'KTB', 1036, CAST(0x0000A9C90118E8F1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADM_SVC', N'0', N'Administration', N'KTB', 1037, CAST(0x0000A9C90118E8F9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmHead', N'2', N'Administration', N'KTB', 1038, CAST(0x0000A9C90118E902 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADMINISTRASI', N'2', N'Administration', N'KTB', 1039, CAST(0x0000A9C90118E90C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'administrasi, front girl', N'2', N'Frontliner/Service Advisor', N'KTB', 1040, CAST(0x0000A9C90118E915 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Administration Service', N'2', N'Administration', N'KTB', 1041, CAST(0x0000A9C90118E91E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADMINSTRASI SERVICE', N'2', N'Administration', N'KTB', 1042, CAST(0x0000A9C90118E928 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmService', N'2', N'Administration', N'KTB', 1043, CAST(0x0000A9C90118E931 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AdmSvc', N'2', N'Administration', N'KTB', 1044, CAST(0x0000A9C90118E93C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ADV & PROMOTION DEPT.', N'1', N'Others', N'KTB', 1045, CAST(0x0000A9C90118E948 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Advertising & Promotion Dept.', N'1', N'Others', N'KTB', 1046, CAST(0x0000A9C90118E953 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Asisten Service Manager', N'2', N'Service Manager', N'KTB', 1047, CAST(0x0000A9C90118E95F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'ass mekanik', N'2', N'Mechanic', N'KTB', 1048, CAST(0x0000A9C90118E96A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Foreman QA', N'1', N'Others', N'KTB', 1049, CAST(0x0000A9C90118E975 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Manager', N'2', N'Others', N'KTB', 1050, CAST(0x0000A9C90118E981 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ass. Mgr', N'2', N'Others', N'KTB', 1051, CAST(0x0000A9C90118E98D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Assistant Foreman (MKM)', N'1', N'Others', N'KTB', 1052, CAST(0x0000A9C90118E996 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'AssMekanik', N'2', N'Mechanic', N'KTB', 1053, CAST(0x0000A9C90118E99F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'BGKL', N'2', N'Others', N'KTB', 1054, CAST(0x0000A9C90118E9A8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair', N'2', N'Others', N'KTB', 1055, CAST(0x0000A9C90118E9B2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair Head', N'2', N'Others', N'KTB', 1056, CAST(0x0000A9C90118E9BB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair leader', N'2', N'Others', N'KTB', 1057, CAST(0x0000A9C90118E9C4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Body Repair mech', N'2', N'Others', N'KTB', 1058, CAST(0x0000A9C90118E9CD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'BodyRepair', N'2', N'Others', N'KTB', 1059, CAST(0x0000A9C90118E9D6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'C.ustomer service', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1060, CAST(0x0000A9C90118E9DF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Chief', N'2', N'Workshop Chief', N'KTB', 1061, CAST(0x0000A9C90118E9E8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Chief W/S', N'2', N'Workshop Chief', N'KTB', 1062, CAST(0x0000A9C90118E9F2 AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Cianjur', N'2', N'Others', N'KTB', 1063, CAST(0x0000A9C90118E9FB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Coordinator', N'2', N'Coordinator', N'KTB', 1064, CAST(0x0000A9C90118EA04 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Coordinator LBU Group', N'2', N'Coordinator', N'KTB', 1065, CAST(0x0000A9C90118EA0E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CRD', N'2', N'Coordinator', N'KTB', 1066, CAST(0x0000A9C90118EA17 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CRD', N'1', N'Coordinator', N'KTB', 1067, CAST(0x0000A9C90118EA20 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1068, CAST(0x0000A9C90118EA31 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'0', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1069, CAST(0x0000A9C90118EA3B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS', N'1', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1070, CAST(0x0000A9C90118EA45 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS dan Front Lady', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1071, CAST(0x0000A9C90118EA4F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS Fuso', N'0', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1072, CAST(0x0000A9C90118EA58 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS Fuso', N'1', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1073, CAST(0x0000A9C90118EA61 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CS Fuso', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1074, CAST(0x0000A9C90118EA6A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1075, CAST(0x0000A9C90118EA73 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'1', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1076, CAST(0x0000A9C90118EA7C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSH', N'0', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1077, CAST(0x0000A9C90118EA84 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'2', N'Others', N'KTB', 1078, CAST(0x0000A9C90118EA8D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'0', N'Others', N'KTB', 1079, CAST(0x0000A9C90118EA96 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CSO', N'1', N'Others', N'KTB', 1080, CAST(0x0000A9C90118EA9E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CUST.SERVICE', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1081, CAST(0x0000A9C90118EAA7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Follow', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1082, CAST(0x0000A9C90118EAB2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Service', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1083, CAST(0x0000A9C90118EABB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Service Sales Dept.', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1084, CAST(0x0000A9C90118EAC4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Customer Svc', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1085, CAST(0x0000A9C90118EACE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CV', N'1', N'Others', N'KTB', 1086, CAST(0x0000A9C90118EAF0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'CV Sect', N'2', N'Others', N'KTB', 1087, CAST(0x0000A9C90118EB0E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Direktur After Sales Service', N'1', N'Others', N'KTB', 1088, CAST(0x0000A9C90118EB18 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Driver', N'2', N'Others', N'KTB', 1089, CAST(0x0000A9C90118EB22 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'DSD', N'1', N'Others', N'KTB', 1090, CAST(0x0000A9C90118EB2B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Export Import Dept.', N'1', N'Others', N'KTB', 1091, CAST(0x0000A9C90118EB35 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F. Checker', N'2', N'Final Checker', N'KTB', 1092, CAST(0x0000A9C90118EB3E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Checker', N'2', N'Final Checker', N'KTB', 1093, CAST(0x0000A9C90118EB48 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Checker & Bodyrepair', N'2', N'Final Checker', N'KTB', 1094, CAST(0x0000A9C90118EB54 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'F.Head', N'2', N'Frontliner/Service Advisor', N'KTB', 1095, CAST(0x0000A9C90118EB5D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Factory Worker (MKM)', N'1', N'Others', N'KTB', 1096, CAST(0x0000A9C90118EB68 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'1', N'Final Checker', N'KTB', 1097, CAST(0x0000A9C90118EB73 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'0', N'Final Checker', N'KTB', 1098, CAST(0x0000A9C90118EB7D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FC', N'2', N'Final Checker', N'KTB', 1099, CAST(0x0000A9C90118EB87 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FCC', N'2', N'Final Checker', N'KTB', 1100, CAST(0x0000A9C90118EB91 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FCC', N'1', N'Final Checker', N'KTB', 1101, CAST(0x0000A9C90118EB9B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Final Checker', N'2', N'Final Checker', N'KTB', 1102, CAST(0x0000A9C90118EBA5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Final Checker Body Repair', N'2', N'Final Checker', N'KTB', 1103, CAST(0x0000A9C90118EBAF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FinalCheck', N'2', N'Final Checker', N'KTB', 1104, CAST(0x0000A9C90118EBB9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'0', N'Frontliner/Service Advisor', N'KTB', 1105, CAST(0x0000A9C90118EBC2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'1', N'Frontliner/Service Advisor', N'KTB', 1106, CAST(0x0000A9C90118EBCB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FL', N'2', N'Frontliner/Service Advisor', N'KTB', 1107, CAST(0x0000A9C90118EBD4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'2', N'Frontliner/Service Advisor', N'KTB', 1108, CAST(0x0000A9C90118EBDD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'0', N'Frontliner/Service Advisor', N'KTB', 1109, CAST(0x0000A9C90118EBE5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FLC', N'1', N'Frontliner/Service Advisor', N'KTB', 1110, CAST(0x0000A9C90118EBEE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'front girl', N'2', N'Frontliner/Service Advisor', N'KTB', 1111, CAST(0x0000A9C90118EBF7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Head', N'2', N'Frontliner/Service Advisor', N'KTB', 1112, CAST(0x0000A9C90118EC00 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT LADIES', N'2', N'Frontliner/Service Advisor', N'KTB', 1113, CAST(0x0000A9C90118EC3C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT LADY', N'1', N'Frontliner/Service Advisor', N'KTB', 1114, CAST(0x0000A9C90118EC49 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Lady', N'2', N'Frontliner/Service Advisor', N'KTB', 1115, CAST(0x0000A9C90118EC6C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT LINE', N'2', N'Frontliner/Service Advisor', N'KTB', 1116, CAST(0x0000A9C90118EC80 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Liner', N'2', N'Frontliner/Service Advisor', N'KTB', 1117, CAST(0x0000A9C90118EC8A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Front Man', N'2', N'Frontliner/Service Advisor', N'KTB', 1118, CAST(0x0000A9C90118EC94 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'FRONT OFFICE', N'2', N'Others', N'KTB', 1119, CAST(0x0000A9C90118EC9E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontgirl/CS', N'2', N'Frontliner/Service Advisor', N'KTB', 1120, CAST(0x0000A9C90118ECA8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontlady', N'2', N'Frontliner/Service Advisor', N'KTB', 1121, CAST(0x0000A9C90118ECB1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Frontman', N'2', N'Frontliner/Service Advisor', N'KTB', 1122, CAST(0x0000A9C90118ECBA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'GM', N'2', N'Others', N'KTB', 1123, CAST(0x0000A9C90118ECC3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Helper', N'2', N'Others', N'KTB', 1124, CAST(0x0000A9C90118ECCC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'HLP', N'2', N'Others', N'KTB', 1125, CAST(0x0000A9C90118ECD4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'HRD ', N'2', N'Others', N'KTB', 1126, CAST(0x0000A9C90118ECDD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Import & Export', N'1', N'Others', N'KTB', 1127, CAST(0x0000A9C90118ECE6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Import & Export Dept.', N'1', N'Others', N'KTB', 1128, CAST(0x0000A9C90118ED02 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS', N'1', N'Instructor', N'KTB', 1129, CAST(0x0000A9C90118ED28 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS', N'2', N'Instructor', N'KTB', 1130, CAST(0x0000A9C90118ED51 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS_C', N'2', N'Instructor', N'KTB', 1131, CAST(0x0000A9C90118ED7F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INSTRUCTURE', N'2', N'Instructor', N'KTB', 1132, CAST(0x0000A9C90118ED94 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Instruktur', N'1', N'Instructor', N'KTB', 1133, CAST(0x0000A9C90118ED9E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'IS', N'2', N'Instructor', N'KTB', 1134, CAST(0x0000A9C90118EDA8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ka. Bengkel', N'2', N'Workshop Chief', N'KTB', 1135, CAST(0x0000A9C90118EDB2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Ka. Service', N'2', N'Service Manager', N'KTB', 1136, CAST(0x0000A9C90118EDBC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'KaCab', N'2', N'Others', N'KTB', 1137, CAST(0x0000A9C90118EDC6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'kader Mekanik', N'2', N'Mechanic', N'KTB', 1138, CAST(0x0000A9C90118EDD1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Kasie. Tools & Equipment', N'2', N'Tools Man', N'KTB', 1139, CAST(0x0000A9C90118EDDA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'kasir', N'2', N'Others', N'KTB', 1140, CAST(0x0000A9C90118EDE2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'KEPALA BENGKEL', N'1', N'Workshop Chief', N'KTB', 1141, CAST(0x0000A9C90118EDEC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Kepala Bengkel', N'2', N'Workshop Chief', N'KTB', 1142, CAST(0x0000A9C90118EDF5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Koordinator daerah', N'2', N'Others', N'KTB', 1143, CAST(0x0000A9C90118EDFE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LC', N'2', N'Others', N'KTB', 1144, CAST(0x0000A9C90118EE07 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LC', N'1', N'Others', N'KTB', 1145, CAST(0x0000A9C90118EE11 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LCV Dept', N'1', N'Others', N'KTB', 1146, CAST(0x0000A9C90118EE1B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LCV Dept.', N'1', N'Others', N'KTB', 1147, CAST(0x0000A9C90118EE25 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'1', N'Leader', N'KTB', 1148, CAST(0x0000A9C90118EE2F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'2', N'Leader', N'KTB', 1149, CAST(0x0000A9C90118EE39 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'LDR', N'0', N'Leader', N'KTB', 1150, CAST(0x0000A9C90118EE43 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'leader', N'2', N'Leader', N'KTB', 1151, CAST(0x0000A9C90118EE4C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader', N'1', N'Leader', N'KTB', 1152, CAST(0x0000A9C90118EE57 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader Body Repair', N'2', N'Others', N'KTB', 1153, CAST(0x0000A9C90118EE61 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader Mekanik', N'2', N'Leader', N'KTB', 1154, CAST(0x0000A9C90118EE6B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader MWS', N'2', N'Leader MWS', N'KTB', 1155, CAST(0x0000A9C90118EE75 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Leader MWS', N'1', N'Leader MWS', N'KTB', 1156, CAST(0x0000A9C90118EE7D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Lubrication', N'2', N'Oil Man', N'KTB', 1157, CAST(0x0000A9C90118EE86 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'M', N'2', N'Others', N'KTB', 1158, CAST(0x0000A9C90118EE8F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Maintenance', N'2', N'Others', N'KTB', 1159, CAST(0x0000A9C90118EE98 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Management Trainee', N'2', N'Others', N'KTB', 1160, CAST(0x0000A9C90118EEA0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Manager', N'1', N'Others', N'KTB', 1161, CAST(0x0000A9C90118EEA9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Manager', N'2', N'Others', N'KTB', 1162, CAST(0x0000A9C90118EEB1 AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'manajer', N'2', N'Others', N'KTB', 1163, CAST(0x0000A9C90118EEBA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MARKETING', N'2', N'Others', N'KTB', 1164, CAST(0x0000A9C90118EEC3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MARKETING SERVICE', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1165, CAST(0x0000A9C90118EECC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Marketing Service/CS Team', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1166, CAST(0x0000A9C90118EED5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MarkSvc', N'2', N'CS Staff (Customer Satisfaction Staff)', N'KTB', 1167, CAST(0x0000A9C90118EEDF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Material Controlling Mks', N'2', N'Others', N'KTB', 1168, CAST(0x0000A9C90118EEE8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'1', N'Mechanic', N'KTB', 1169, CAST(0x0000A9C90118EEF1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'0', N'Mechanic', N'KTB', 1170, CAST(0x0000A9C90118EEFA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MC', N'2', N'Mechanic', N'KTB', 1171, CAST(0x0000A9C90118EF03 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekaniik', N'2', N'Mechanic', N'KTB', 1172, CAST(0x0000A9C90118EF0C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik', N'2', N'Mechanic', N'KTB', 1173, CAST(0x0000A9C90118EF15 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik', N'1', N'Mechanic', N'KTB', 1174, CAST(0x0000A9C90118EF1E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK BODY REPAIR', N'2', N'Others', N'KTB', 1175, CAST(0x0000A9C90118EF27 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Body Repair Dept.', N'2', N'Others', N'KTB', 1176, CAST(0x0000A9C90118EF30 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik cab Duri', N'2', N'Others', N'KTB', 1177, CAST(0x0000A9C90118EF39 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK Cabang Simpang', N'2', N'Others', N'KTB', 1178, CAST(0x0000A9C90118EF42 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK HELPER', N'2', N'Mechanic', N'KTB', 1179, CAST(0x0000A9C90118EF4B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik J', N'2', N'Mechanic', N'KTB', 1180, CAST(0x0000A9C90118EF55 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Junior', N'2', N'Mechanic', N'KTB', 1181, CAST(0x0000A9C90118EF5E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik MTB 2', N'2', N'Mechanic', N'KTB', 1182, CAST(0x0000A9C90118EF67 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MEKANIK MWS', N'2', N'Mechanic', N'KTB', 1183, CAST(0x0000A9C90118EF70 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik OJT', N'2', N'Mechanic', N'KTB', 1184, CAST(0x0000A9C90118EF78 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik PDC', N'2', N'Mechanic', N'KTB', 1185, CAST(0x0000A9C90118EF81 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Senior', N'2', N'Mechanic', N'KTB', 1186, CAST(0x0000A9C90118EF8A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Trainee', N'2', N'Mechanic', N'KTB', 1187, CAST(0x0000A9C90118EF94 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Mekanik Yunior', N'2', N'Mechanic', N'KTB', 1188, CAST(0x0000A9C90118EF9D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'1', N'Mechanic', N'KTB', 1189, CAST(0x0000A9C90118EFA6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'0', N'Mechanic', N'KTB', 1190, CAST(0x0000A9C90118EFAE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MKN', N'2', N'Mechanic', N'KTB', 1191, CAST(0x0000A9C90118EFB7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MT', N'2', N'Others', N'KTB', 1192, CAST(0x0000A9C90118EFC0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'MW', N'2', N'Others', N'KTB', 1193, CAST(0x0000A9C90118EFC9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Office Boy', N'2', N'Others', N'KTB', 1194, CAST(0x0000A9C90118EFD2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Oil Man', N'2', N'Oil Man', N'KTB', 1195, CAST(0x0000A9C90118EFDB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OilLeader', N'2', N'Oil Man', N'KTB', 1196, CAST(0x0000A9C90118EFE4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Oilman', N'2', N'Oil Man', N'KTB', 1197, CAST(0x0000A9C90118EFED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'1', N'Oil Man', N'KTB', 1198, CAST(0x0000A9C90118EFF6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'0', N'Oil Man', N'KTB', 1199, CAST(0x0000A9C90118EFFF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OM', N'2', N'Oil Man', N'KTB', 1200, CAST(0x0000A9C90118F007 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'1', N'Others', N'KTB', 1201, CAST(0x0000A9C90118F010 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'2', N'Others', N'KTB', 1202, CAST(0x0000A9C90118F018 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'OTH', N'0', N'Others', N'KTB', 1203, CAST(0x0000A9C90118F021 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Other', N'2', N'Others', N'KTB', 1204, CAST(0x0000A9C90118F02A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Other', N'1', N'Others', N'KTB', 1205, CAST(0x0000A9C90118F032 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Others', N'2', N'Others', N'KTB', 1206, CAST(0x0000A9C90118F03B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Others', N'1', N'Others', N'KTB', 1207, CAST(0x0000A9C90118F044 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'P_Mekanik', N'2', N'Mechanic', N'KTB', 1208, CAST(0x0000A9C90118F04D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PART', N'2', N'Others', N'KTB', 1209, CAST(0x0000A9C90118F056 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Counter', N'2', N'Others', N'KTB', 1210, CAST(0x0000A9C90118F05F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Logistic Dept.', N'1', N'Others', N'KTB', 1211, CAST(0x0000A9C90118F068 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Runner', N'2', N'Others', N'KTB', 1212, CAST(0x0000A9C90118F070 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Part Runner Svc', N'2', N'Others', N'KTB', 1213, CAST(0x0000A9C90118F079 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'partruner', N'2', N'Others', N'KTB', 1214, CAST(0x0000A9C90118F083 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Partrunner', N'2', N'Others', N'KTB', 1215, CAST(0x0000A9C90118F08B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Parts Logistic Dept', N'1', N'Others', N'KTB', 1216, CAST(0x0000A9C90118F094 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Parts Logistic Dept.', N'1', N'Others', N'KTB', 1217, CAST(0x0000A9C90118F09D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'partsrunner', N'2', N'Others', N'KTB', 1218, CAST(0x0000A9C90118F0A5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PC LCV', N'1', N'Others', N'KTB', 1219, CAST(0x0000A9C90118F0AE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PC STRATEGY DEPT.', N'1', N'Others', N'KTB', 1220, CAST(0x0000A9C90118F0B7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PCD', N'1', N'Others', N'KTB', 1221, CAST(0x0000A9C90118F0C1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Pembantu Mekanik', N'2', N'Mechanic', N'KTB', 1222, CAST(0x0000A9C90118F0C9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PembMekanik', N'2', N'Mechanic', N'KTB', 1223, CAST(0x0000A9C90118F0D1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Penanggung Jawab Service', N'2', N'Others', N'KTB', 1224, CAST(0x0000A9C90118F0DA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Penj. Service', N'2', N'Others', N'KTB', 1225, CAST(0x0000A9C90118F0E4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PENSIUN DINI', N'2', N'Others', N'KTB', 1226, CAST(0x0000A9C90118F0ED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Pjs. LEADER', N'2', N'Leader', N'KTB', 1227, CAST(0x0000A9C90118F0F6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PQR/WSC & CS TEAM', N'2', N'Others', N'KTB', 1228, CAST(0x0000A9C90118F0FF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PR', N'2', N'Others', N'KTB', 1229, CAST(0x0000A9C90118F108 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'PR Office', N'1', N'Others', N'KTB', 1230, CAST(0x0000A9C90118F111 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Procurement Dept.', N'1', N'Others', N'KTB', 1231, CAST(0x0000A9C90118F119 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Production Control Dept.', N'1', N'Others', N'KTB', 1232, CAST(0x0000A9C90118F122 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Q. Assurance', N'1', N'Others', N'KTB', 1233, CAST(0x0000A9C90118F12B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'QC', N'2', N'Others', N'KTB', 1234, CAST(0x0000A9C90118F134 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Quality Assurance', N'1', N'Others', N'KTB', 1235, CAST(0x0000A9C90118F13D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Quality Control', N'1', N'Others', N'KTB', 1236, CAST(0x0000A9C90118F146 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Rental Marketing Dept.', N'1', N'Others', N'KTB', 1237, CAST(0x0000A9C90118F14F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Retail Sales Development Dept.', N'1', N'Others', N'KTB', 1238, CAST(0x0000A9C90118F157 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Roadman', N'1', N'Others', N'KTB', 1239, CAST(0x0000A9C90118F160 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'RSD', N'1', N'Others', N'KTB', 1240, CAST(0x0000A9C90118F168 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'S/P', N'2', N'Others', N'KTB', 1241, CAST(0x0000A9C90118F171 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SALES', N'2', N'Others', N'KTB', 1242, CAST(0x0000A9C90118F179 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Department', N'2', N'Others', N'KTB', 1243, CAST(0x0000A9C90118F182 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Planning', N'1', N'Others', N'KTB', 1244, CAST(0x0000A9C90118F18B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Planning Dept.', N'1', N'Others', N'KTB', 1245, CAST(0x0000A9C90118F193 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Processing Dept.', N'1', N'Others', N'KTB', 1246, CAST(0x0000A9C90118F19C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Sales Supervisor', N'2', N'Others', N'KTB', 1247, CAST(0x0000A9C90118F1A5 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SBM', N'2', N'Others', N'KTB', 1248, CAST(0x0000A9C90118F1AE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SC', N'2', N'Others', N'KTB', 1249, CAST(0x0000A9C90118F1B7 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SCN', N'2', N'Others', N'KTB', 1250, CAST(0x0000A9C90118F1BF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Section Chief', N'1', N'Others', N'KTB', 1251, CAST(0x0000A9C90118F1C9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'section head - acct', N'1', N'Others', N'KTB', 1252, CAST(0x0000A9C90118F1D2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'service advicer', N'2', N'Frontliner/Service Advisor', N'KTB', 1253, CAST(0x0000A9C90118F1DB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service adviser', N'2', N'Frontliner/Service Advisor', N'KTB', 1254, CAST(0x0000A9C90118F1E4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SERVICE ADVISOR', N'2', N'Frontliner/Service Advisor', N'KTB', 1255, CAST(0x0000A9C90118F1ED AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Advisor 01', N'2', N'Frontliner/Service Advisor', N'KTB', 1256, CAST(0x0000A9C90118F1F6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SERVICE DEPT.', N'2', N'Others', N'KTB', 1257, CAST(0x0000A9C90118F1FF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Head', N'2', N'Service Manager', N'KTB', 1258, CAST(0x0000A9C90118F208 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Service Manager', N'2', N'Service Manager', N'KTB', 1259, CAST(0x0000A9C90118F211 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SL', N'2', N'Others', N'KTB', 1260, CAST(0x0000A9C90118F219 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SLM', N'0', N'Others', N'KTB', 1261, CAST(0x0000A9C90118F222 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SP', N'2', N'Others', N'KTB', 1262, CAST(0x0000A9C90118F22A AS DateTime))
GO
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare part', N'2', N'Others', N'KTB', 1263, CAST(0x0000A9C90118F233 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare part adm', N'2', N'Others', N'KTB', 1264, CAST(0x0000A9C90118F23C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Spare Part Department', N'2', N'Others', N'KTB', 1265, CAST(0x0000A9C90118F244 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'spare Parts', N'2', N'Others', N'KTB', 1266, CAST(0x0000A9C90118F24D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Spare Parts Gudang', N'2', N'Others', N'KTB', 1267, CAST(0x0000A9C90118F255 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPAREPART', N'2', N'Others', N'KTB', 1268, CAST(0x0000A9C90118F25E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SpareParts', N'2', N'Others', N'KTB', 1269, CAST(0x0000A9C90118F267 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPR_MGR', N'2', N'Others', N'KTB', 1270, CAST(0x0000A9C90118F270 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPV', N'1', N'Others', N'KTB', 1271, CAST(0x0000A9C90118F279 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SPV', N'2', N'Others', N'KTB', 1272, CAST(0x0000A9C90118F282 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff', N'2', N'Others', N'KTB', 1273, CAST(0x0000A9C90118F28C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff', N'1', N'Others', N'KTB', 1274, CAST(0x0000A9C90118F295 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff (ADV & PROMOTION DEPT.)', N'1', N'Others', N'KTB', 1275, CAST(0x0000A9C90118F29E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'STAFF (MARKET ANALYSIS DEPT.)', N'1', N'Others', N'KTB', 1276, CAST(0x0000A9C90118F2A6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - acct', N'1', N'Others', N'KTB', 1277, CAST(0x0000A9C90118F2AF AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - GA Dept.', N'1', N'Others', N'KTB', 1278, CAST(0x0000A9C90118F2B8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - legal dept', N'1', N'Others', N'KTB', 1279, CAST(0x0000A9C90118F2C0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - QC', N'1', N'Others', N'KTB', 1280, CAST(0x0000A9C90118F2C9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff - RSD', N'1', N'Others', N'KTB', 1281, CAST(0x0000A9C90118F2D2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff - Sales Processing Dept.', N'1', N'Others', N'KTB', 1282, CAST(0x0000A9C90118F2DB AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff -GA', N'1', N'Others', N'KTB', 1283, CAST(0x0000A9C90118F2E3 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff -RSD', N'1', N'Others', N'KTB', 1284, CAST(0x0000A9C90118F2EC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'STAFF BODY REPAIR', N'2', N'Others', N'KTB', 1285, CAST(0x0000A9C90118F2F4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff Penagihan Service', N'2', N'Others', N'KTB', 1286, CAST(0x0000A9C90118F2FD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff QC', N'1', N'Others', N'KTB', 1287, CAST(0x0000A9C90118F305 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff RSD', N'1', N'Others', N'KTB', 1288, CAST(0x0000A9C90118F30E AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff WSC', N'1', N'Others', N'KTB', 1289, CAST(0x0000A9C90118F317 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Staff- GA', N'1', N'Others', N'KTB', 1290, CAST(0x0000A9C90118F31F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'staff-PRD', N'1', N'Others', N'KTB', 1291, CAST(0x0000A9C90118F328 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Strada Triton Office', N'1', N'Others', N'KTB', 1292, CAST(0x0000A9C90118F330 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SUPERVISOR', N'2', N'Others', N'KTB', 1293, CAST(0x0000A9C90118F339 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC', N'1', N'Frontliner/Service Advisor', N'KTB', 1294, CAST(0x0000A9C90118F341 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'svc adv', N'2', N'Frontliner/Service Advisor', N'KTB', 1295, CAST(0x0000A9C90118F34A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Adv.', N'2', N'Frontliner/Service Advisor', N'KTB', 1296, CAST(0x0000A9C90118F353 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC Coordination', N'2', N'Coordinator', N'KTB', 1297, CAST(0x0000A9C90118F35B AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC COORDINATOOR', N'2', N'Coordinator', N'KTB', 1298, CAST(0x0000A9C90118F364 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Head', N'2', N'Service Manager', N'KTB', 1299, CAST(0x0000A9C90118F36D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Koordinator Jatim & Bali', N'2', N'Coordinator', N'KTB', 1300, CAST(0x0000A9C90118F38D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC MANAGER', N'1', N'Service Manager', N'KTB', 1301, CAST(0x0000A9C90118F39D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc Mgr', N'2', N'Service Manager', N'KTB', 1302, CAST(0x0000A9C90118F3B2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC SPV', N'2', N'Service Manager', N'KTB', 1303, CAST(0x0000A9C90118F3C0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Svc. Head', N'2', N'Service Manager', N'KTB', 1304, CAST(0x0000A9C90118F3CA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC. Koordinator', N'2', N'Coordinator', N'KTB', 1305, CAST(0x0000A9C90118F3D4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_HEAD', N'2', N'Workshop Chief', N'KTB', 1306, CAST(0x0000A9C90118F3DD AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'2', N'Service Manager', N'KTB', 1307, CAST(0x0000A9C90118F3E6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'0', N'Service Manager', N'KTB', 1308, CAST(0x0000A9C90118F3F0 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR', N'1', N'Service Manager', N'KTB', 1309, CAST(0x0000A9C90118F3F9 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR_C', N'1', N'Service Manager', N'KTB', 1310, CAST(0x0000A9C90118F402 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR_C', N'0', N'Service Manager', N'KTB', 1311, CAST(0x0000A9C90118F40C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_MGR_C', N'2', N'Service Manager', N'KTB', 1312, CAST(0x0000A9C90118F416 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'SVC_S', N'2', N'Others', N'KTB', 1313, CAST(0x0000A9C90118F420 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Technical', N'1', N'Others', N'KTB', 1314, CAST(0x0000A9C90118F42A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Technical Dept.', N'1', N'Others', N'KTB', 1315, CAST(0x0000A9C90118F435 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'technical supervisor', N'2', N'Others', N'KTB', 1316, CAST(0x0000A9C90118F43F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TK', N'2', N'Others', N'KTB', 1317, CAST(0x0000A9C90118F449 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'2', N'Tools Man', N'KTB', 1318, CAST(0x0000A9C90118F453 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'1', N'Tools Man', N'KTB', 1319, CAST(0x0000A9C90118F45C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TM', N'0', N'Tools Man', N'KTB', 1320, CAST(0x0000A9C90118F466 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TOOL MAN', N'2', N'Tools Man', N'KTB', 1321, CAST(0x0000A9C90118F470 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolkeep', N'2', N'Tools Man', N'KTB', 1322, CAST(0x0000A9C90118F479 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolkeep.', N'2', N'Tools Man', N'KTB', 1323, CAST(0x0000A9C90118F482 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Tools Keeper', N'2', N'Tools Man', N'KTB', 1324, CAST(0x0000A9C90118F48C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Tools Man', N'2', N'Tools Man', N'KTB', 1325, CAST(0x0000A9C90118F494 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TOOLS ROOM', N'2', N'Tools Man', N'KTB', 1326, CAST(0x0000A9C90118F49D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolskeep', N'2', N'Tools Man', N'KTB', 1327, CAST(0x0000A9C90118F4A6 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolsman', N'2', N'Tools Man', N'KTB', 1328, CAST(0x0000A9C90118F4AE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Toolsman & Maintenance', N'2', N'Tools Man', N'KTB', 1329, CAST(0x0000A9C90118F4B8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRAINER', N'2', N'Instructor', N'KTB', 1330, CAST(0x0000A9C90118F4C1 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRAINING CHIEF', N'2', N'Instructor', N'KTB', 1331, CAST(0x0000A9C90118F4CA AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Training Dept.', N'1', N'Others', N'KTB', 1332, CAST(0x0000A9C90118F4D4 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Training Support', N'1', N'Others', N'KTB', 1333, CAST(0x0000A9C90118F4DE AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRUCK & BUS Dept.', N'1', N'Others', N'KTB', 1334, CAST(0x0000A9C90118F4E8 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'TRUCK&BUS', N'1', N'Others', N'KTB', 1335, CAST(0x0000A9C90118F4F2 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VC Dept CBU Import', N'1', N'Others', N'KTB', 1336, CAST(0x0000A9C90118F4FC AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VC Dept.', N'1', N'Others', N'KTB', 1337, CAST(0x0000A9C90118F506 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Vehicle Control', N'1', N'Others', N'KTB', 1338, CAST(0x0000A9C90118F50F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'VR', N'2', N'Others', N'KTB', 1339, CAST(0x0000A9C90118F51A AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'W/s Chief', N'2', N'Workshop Chief', N'KTB', 1340, CAST(0x0000A9C90118F524 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WORKSHOP CHIEF', N'2', N'Workshop Chief', N'KTB', 1341, CAST(0x0000A9C90118F52D AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WORKSHOP HEAD', N'2', N'Workshop Chief', N'KTB', 1342, CAST(0x0000A9C90118F538 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'Workshop Sect.Head', N'2', N'Workshop Chief', N'KTB', 1343, CAST(0x0000A9C90118F541 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF', N'1', N'Workshop Chief', N'KTB', 1344, CAST(0x0000A9C90118F54C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF', N'2', N'Workshop Chief', N'KTB', 1345, CAST(0x0000A9C90118F557 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'2', N'Workshop Chief', N'KTB', 1346, CAST(0x0000A9C90118F562 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'0', N'Workshop Chief', N'KTB', 1347, CAST(0x0000A9C90118F56C AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WRK_CHF_C', N'1', N'Workshop Chief', N'KTB', 1348, CAST(0x0000A9C90118F578 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WS Chief', N'2', N'Workshop Chief', N'KTB', 1349, CAST(0x0000A9C90118F584 AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'WSC-TRAINING SECTION', N'2', N'Others', N'KTB', 1350, CAST(0x0000A9C90118F58F AS DateTime))
INSERT [dbo].[JobPositionMapping] ([JobPosition], [Status], [NewJobPosition], [Company], [ID], [CreatedDate]) VALUES (N'INS_C', N'0', N'Instructor', N'MMKSI', 1351, CAST(0x0000A9CA00995A9B AS DateTime))
SET IDENTITY_INSERT [dbo].[JobPositionMapping] OFF
ALTER TABLE [dbo].[JobPositionMapping] ADD  CONSTRAINT [DF_JobPositionMapping_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


UPDATE A SET A.JobPosition = x.Code, LastUpdateBy = 'Admin-Migration', LastUpdateTime = GETDATE() FROM dbo.[TrTrainee] A
OUTER APPLY
(
	SELECT TOP 1 b.id, C.Code, C.Description FROM [JobPositionMapping] B 
	LEFT JOIN dbo.JobPosition C ON B.NewJobPosition = c.Description
	WHERE A.JobPosition = B.JobPosition AND B.Company = 'MMKSI' and ISNULL(NewJobPosition,'') <> ''
	ORDER BY C.RowStatus DESC, b.Status ASC --biar ambil yang  aktif dulu, lalu cek yang statusnya bukan 2 dulu (2 : inactive)
)x
where x.id is not null 


drop table JobPositionMapping


---Non Aktif  old Jobposition service
Update JobPosition set rowstatus =-1 where category=2

---Insert New Job Position----
INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_MGR'
           ,'Service Manager'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_WRK_CHF'
           ,'Workshop Head'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_SA'
           ,'Service Advisor'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_ADM'
           ,'Administration'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_CS'
           ,'CS Staff (Customer Satisfaction Staff)'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_FC'
           ,'Final Checker'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_LDR'
           ,'Leader'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_MKN'
           ,'Mechanic'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_MHSK'
           ,'Mechanic HSK'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO


INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_OM'
           ,'Oil Man'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_TM'
           ,'Tools Man'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_WM'
           ,'Washing Man'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('SVC_INS'
           ,'Instructor'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPosition]
           ([Code]
           ,[Description]
           ,[Category]
           ,[SalesTarget]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('OTH'
           ,'Other'
           ,2
           ,0
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO


---Non Aktif  old Jobpositiontomenu service
Update JobPositionToMenu set rowstatus =-1 where JobPositionMenuId=5

---Insert New JobPositionToMenu----
INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_MGR' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_WRK_CHF' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_SA' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_ADM' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_CS' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO


INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_FC' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_LDR' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_MKN' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_MHSK' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_OM' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_TM' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_WM' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='SVC_INS' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO

INSERT INTO [dbo].[JobPositionToMenu]
           ([JobPositionID]
           ,[JobPositionMenuId]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ((select id from jobposition where code='OTH' and rowstatus=0 and category=2)
           ,5
           ,0
           ,'admin-migration'
           ,GetDate()
           ,'admin-migration'
           ,GetDate())
GO