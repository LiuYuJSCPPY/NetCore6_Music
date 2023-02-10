# NetCore6 MVC音樂平台

![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E9%A6%96%E9%A0%81.PNG)

## 專案中使用的技術與工具
**前端:**

1.  HTML5
2.  CSS3
3.  jquery
4.  AJAX
5.  Razor 

**後端:**
 1.  C#
 2.  ASP.NET Core6 MVC
 3.  Entity Framework Core 6.0.13
 4.  Linq
 5.  Hashids.net
 6.  TagLibSharp
 7.  NToastNotify
 8.  ASP.NET Core6 Identity

**資料庫:**
1.  MSSQL

## 功能

### 前端
* 前端頁面 ( 未使用會員 ) :
  * 首頁頁面 : 在首頁中掏出10個專輯以及藝人作為首頁。
  * 單專輯頁面 : 單個專輯頁面該專輯的歌曲。
  * 藝人頁面 : 藝人的頁面中會有藝人的專輯、熱門的歌曲以及藝人的個人資料。
  * 藝人所有專輯 : 藝人所有專輯頁面所有專輯的歌曲。
  
* 前端頁面 ( 使用會員 ) :
  * 首頁頁面 : 在首頁中掏出10個專輯以及藝人作為首頁。
  * 單專輯頁面 : 單個專輯頁面該專輯的歌曲。
  * 藝人頁面 : 藝人的頁面中會有藝人的專輯、熱門的歌曲以及藝人的個人資料。
  * 藝人所有專輯 : 藝人所有專輯頁面所有專輯的歌曲。
  * 你的音樂庫 : 你自己的專屬音樂庫還有你喜歡的音樂。
  * 關注專輯 : 點選你有關注的專輯。
  * 關注藝人 : 點選你有關注的藝人。
  
 ### 後端 :
 * 管理藝人頁面 : 顯示所有藝人的頁面(新增、更新、刪除)。
 * 管理藝人的專輯頁面 : 只會出現該藝人的所有專輯(新增、更新、刪除)。
 * 專輯歌曲頁面 : 專輯裡面所有歌曲(新增、更新、刪除)。
 
 ## 展示專案圖片 : 
 **前端頁面(未使用會員):**
1. 首頁 :
   1. 挑出10個專輯。
   2. 挑出10個藝人。
   3. 首頁中有打招呼 (12點以前 : 早安 、 12點至17點 : 午安 、 17點以後 : 晚安 ) 。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E9%A6%96%E9%A0%81.PNG)

2. 單專輯頁面:
   1.  單個專輯頁面該專輯的歌曲。
   2.  該專輯總共時間長度。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E6%9C%AA%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E5%B0%88%E8%BC%AF.PNG)
  
3. 藝人頁面 : 
   1.  藝人的頁面中會有藝人的專輯、熱門的歌曲以及藝人的個人資料。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E6%9C%AA%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E8%97%9D%E4%BA%BA1.PNG)
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E6%9C%AA%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E8%97%9D%E4%BA%BA2.PNG)
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E6%9C%AA%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E8%97%9D%E4%BA%BA3.PNG)
4. 藝人所有專輯 : 
   1.  藝人所有專輯頁面所有專輯的歌曲。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E6%9C%AA%E7%99%BB%E5%85%A5%E8%97%9D%E4%BA%BA%E6%89%80%E6%9C%89%E5%B0%88%E8%BC%AF.PNG)
  
**前端頁面(使用會員):**
1. **首頁頁面:** 
   1. 挑出10個專輯。
   2. 挑出10個藝人。
   3. 首頁中有打招呼 (12點以前 : 早安 、 12點至17點 : 午安 、 17點以後 : 晚安 ) 。
   4. 建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E9%A6%96%E9%A0%81.PNG)
2. **單專輯頁面:** 
   1.  單個專輯頁面該專輯的歌曲。
   2.  該專輯總共時間長度。
   3.  點選關注該專輯(使用的是AJAX)。
   4.  點選喜歡歌曲(使用的是AJAX)。
   5.  建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E5%B0%88%E8%BC%AF.PNG)
3. **藝人頁面:**
   1.  藝人的頁面中會有藝人的專輯、熱門的歌曲以及藝人的個人資料。
   2.  點選喜歡歌曲(使用的是AJAX)。
   3.  點選關注的藝人(使用的是AJAX)。
   4.  建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E8%97%9D%E4%BA%BA1.PNG)
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E5%89%8D%E5%8F%B0%E8%97%9D%E4%BA%BA2.PNG)
4. **藝人所有專輯:**
   1.  藝人所有專輯頁面所有專輯的歌曲。
   3.  點選關注該專輯(使用的是AJAX)。
   4.  點選喜歡歌曲(使用的是AJAX)。
   5.  建立播放清單(使用的是AJAX)。
   
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E7%99%BB%E5%85%A5%E8%97%9D%E4%BA%BA%E6%89%80%E6%9C%89%E5%B0%88%E8%BC%AF.PNG)
5. **你的音樂庫:** 
   1.  你自己的專屬音樂庫。
   2.  你喜歡的音樂。
   3.  建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E9%9F%B3%E6%A8%82%E5%BA%AB.PNG)
6. **個別的音樂庫:**
   1.  單個播放清單頁面該播放清單的歌曲。
   2.  該播放清單總共時間長度。
   3.  點選喜歡歌曲(使用的是AJAX)。
   4.  搜尋歌曲新增到播放清單(使用的是AJAX)。
   5.  取消播放清單的歌曲(使用的是AJAX)。
   6.  建立播放清單(使用的是AJAX)。
   7.  更新播放清單(使用的是AJAX)。
   8.  刪除播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E5%80%8B%E5%88%A5%E9%9F%B3%E6%A8%82%E5%BA%AB.PNG)

7. **喜歡的歌曲**
   1.  建立播放清單(使用的是AJAX)。
   2.  該喜歡的歌曲總共時間長度。
   3.  取消喜歡的歌曲(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E5%96%9C%E6%AD%A1%E7%9A%84%E6%AD%8C%E6%9B%B2.PNG)

8. **關注專輯:**
   1.  點選你有關注的所有專輯。
   2.  建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E9%97%9C%E6%B3%A8%E7%9A%84%E5%B0%88%E8%BC%AF.PNG)
7. **關注藝人:** 
   1.  點選你有關注的所有藝人。
   2.  建立播放清單(使用的是AJAX)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E9%97%9C%E6%B3%A8%E7%9A%84%E8%97%9D%E4%BA%BA.PNG)


  
**後端端頁面:**
1. **管理藝人頁面:** 顯示所有藝人的頁面(新增、更新、刪除)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E5%BE%8C%E8%87%BA%E7%AE%A1%E7%90%86%E8%97%9D%E4%BA%BACRUD.PNG)
2. **管理藝人的專輯頁面:** 只會出現該藝人的所有專輯(新增、更新、刪除)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E5%BE%8C%E5%8F%B0%E5%B0%88%E8%BC%AFCRUD.PNG)
3. **專輯歌曲頁面:** 專輯裡面所有歌曲(新增、更新、刪除)。
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/%E5%BE%8C%E5%8F%B0%E5%B0%88%E8%BC%AF%E9%9F%B3%E6%A8%82CRUD.PNG)


## 資料庫關聯圖
![image](https://github.com/LiuYuJSCPPY/NetCore6_Music/blob/main/Iamge/Untitled%20(1).png)

## 前端使用範例影片

https://user-images.githubusercontent.com/73396015/218117717-8859fedd-73dc-46c3-a360-f787e9770fc1.mp4

## 後端使用範例影片

https://user-images.githubusercontent.com/73396015/218117815-0ebe24f3-f663-43d2-a432-a4f6d9316d1d.mp4

