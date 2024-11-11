# python crawler 爬環境相關資料
# 參考資料 https://www.youtube.com/watch?v=1GUkiJSIh8k

# 1.
import requests
import json

# 2. GET
# baseUrl = http://data.epa.gov.tw/api/v2。
# aqx_p_492: 資料集代碼。
# limit=50: 回傳資料上限值。
# format=json: JSON 資料。
# api_key= : 申請的 API KEY。
# filters=: 篩選資料。
# flareno,EQ,A002: 欄位(廢氣燃燒塔編號)，匹配 A002  的。
# |: 且。
# itemdesc,EQ,烯烴-異丁烯、其他-C-2 化合物: 欄位(監測項目名稱), 匹配 烯烴-異丁烯、其他-C-2 化合物的。
url = 'http://data.epa.gov.tw/api/v2/aqx_p_492?limit=50&format=json&api_key=9d....&filters=flareno,EQ,A002|itemdesc,EQ,烯烴-異丁烯、其他-C-2 化合物'

response = requests.get(url)
json_data = json.loads(response.text)

for data in json_data["records"]: # records 是資料集字典名稱
    m_time      = data["m_time"] #欄位 監測時間
    itemdesc    = data["itemdesc"] #欄位 監測項目名稱(烯烴-異丁烯、其他-C-2 化合物)
    m_val       = data["m_val"] #欄位 監測數值
    code2       = data["code2"] #欄位 資料辨識碼

    print(m_time, itemdesc, m_val, code2)

#3. 寫進資料庫
