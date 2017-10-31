# ShpConverter 
=============

### 用途
ShpConverter是用来将shapefile文件转换为对应的GeoJSON格式的字符串的.NET(C#)类库。

### 项目信息
+ 基于.Net Framework 4.0
+ 目前版本为 v1.3.0.0
+ 基于开源类库 DotSpatial v1.9 而开发。
+ 支持将点、多点、线、多线、以及面图层转换为GeoJSON。

### 使用方法
```IFeatureSet fs = FeatureSet.Open(shpPath);  
IShpConverter shpConverter = new ConverterToJson();  
string geoJson = shpConverter.GetGeoJson(fs);  ```

### 注意事项
ShpConverter仅用来将shapefile文件转换为GeoJSON格式，适合于GIS开发者或者与GIS相关的项目开发使用。

### TODO
对多面图层转换为GeoJSON的支持。

### 开发者
zhaiguang，zhaiguang145@gmail.com

### 许可协议
有关许可条款的更多信息，请参阅LICENSE.txt。
