
export default class NewsService {
    private url = "/newsapi";
    
    async getNews() {
        let result = await fetch(this.url).then(response => response.json());
        console.log(result);
        return result.articles;
    }
}