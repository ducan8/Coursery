export interface User {
  userName: string;
  fullName: string;
  password: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: Date;
  createTime: Date;
  updateTime: Date;
  avatar: string;
  userStatus: string;
  isActive: boolean;
  address: string;
  instruction: string;
}

// public string UserName { get; set; } = String.Empty;
// public string FullName { get; set; } = String.Empty;
// public string Password { get; set; } = String.Empty;
// public string Email { get; set; } = String.Empty;
// public string PhoneNumber { get; set; } = String.Empty;
// public DateTime DateOfBirth { get; set; }
// public DateTime CreateTime { get; set; } = DateTime.Now;
// public DateTime? UpdateTime { get; set; }
// public string Avatar { get; set; } = String.Empty;
// public ConstantEnums.UserStatus UserStatus { get; set; } = ConstantEnums.UserStatus.UnActived;
// public bool IsActive { get; set; }
// public string Address { get; set; } = string.Empty;
