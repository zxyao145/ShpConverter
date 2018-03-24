# ShpConverter 
=============

### 用途
ShpConverter是用来将shapefile文件转换为对应的GeoJSON格式的字符串的.NET(C#)类库。

### 项目信息
+ 基于.Net Framework 4.5
+ 目前版本为 v2.0.0.0
+ 基于开源类库 DotSpatial v1.9 和 Jil 而开发。
+ 支持将点、多点、线、多线、以及面shapefile图层转换为GeoJSON。

### 更新信息
重构了全部代码，修改了些bug（感谢道友“芥末油”）。


### 使用方法
```
IFeatureSet fs = FeatureSet.Open(shpPath);  
IShpConvert shpConvert =new ShpConvert(fs);  
string geoJson = shpConvert.ToGeoJSON();
```

### 注意事项
+ ShpConverter仅用来将shapefile文件转换为GeoJSON格式，适合于GIS开发者或者与GIS相关的项目开发使用。
+ ShpConverter仅支持WGS84地理坐标系

### 开发者
zxyao145

### 许可协议
有关许可条款的更多信息，请参阅LICENSE.txt。
