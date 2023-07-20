<template>
  <div>
    <el-card class="box-card" v-if="list.article !== undefined">
      <div>
        <div slot="header" class="head">
          <span> {{ list.article.title }}</span>
        </div>

        <p v-html="list.article.content.words"></p>

        <div style="width: 200px; margin-left: 500px">
          <p>{{ list.article.user.username }}</p>
          <p>{{ list.article.updatedTime }}</p>
        </div>
      </div>

      <div v-if="getCommentFrom !== undefined">
        <div v-for="comment in getCommentFrom" :key="comment.id">
          <div v-for="u in user" :key="u.id">
            <!-- //判断当前的评论文章的用户==用户 -->
            <div v-if="comment.fromUserId == u.id">
              {{ u.username }} <span>：{{ comment.comment }}</span>
            </div>
          </div>
        </div>
      </div>

      <div class="fool">
        <el-button
          v-if="!zan"
          type="success"
          icon="el-icon-thumb"
          circle
          @click="isZan(zanFrom)"
        ></el-button>
        <el-button
          v-else
          icon="el-icon-thumb"
          circle
          @click="CancelZan(zanFrom)"
        ></el-button>
        {{ list.article.likesNum }}

        <el-button type="warning" icon="el-icon-view" circle></el-button
        >浏览量:{{ list.article.readingsNum }}

        <el-button type="text" @click="com">评论</el-button>
      </div>
    </el-card>
  </div>
</template>

<script>
import { getbyId, putReadNUm } from "../api/article";
import { comment, getComment } from "../api/comment.js";
import { isLike } from "../api/likes.js";
import { getUserId } from "../utils/auth";

export default {
  data() {
    return {
      user: [],
      userId: "",
      zan: false,
      id: 0,
      list: [],
      zanFrom: {
        UserId: 0,
        ArticleId: 0,
      },
      //评论
      comFrom: {
        FromUserId: 0,
        ToUserId: 0,
        ArticleId: 0,
        Comment: "",
      },
      //获取评论内容
      getCommentFrom: [],
    };
  },
  methods: {
    //点赞触发的事件
    isZan(msg) {
      this.zan = !this.zan;
      this.list.article.likesNum += 1;

      // 点赞接口
      isLike(msg)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },

    // 取消点赞
    CancelZan(msg) {
      this.zan = !this.zan;
      this.list.article.likesNum -= 1;
      // 点赞接口
      isLike(msg)
        .then((res) => {
          console.log(res);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    //评论时
    com() {
      this.$prompt("请输入你评论的内容", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
      })
        .then(({ value }) => {
          this.$message({
            type: "success",
            message: "评论成功",
          });

          this.comFrom.Comment = value;
          this.comFrom.FromUserId = this.userId;
          this.comFrom.ToUserId = this.list.article.userId;
          this.comFrom.ArticleId = this.id;

          console.log(this.comFrom, "xixi");
          console.log("你的评论是: " + value);

          //调用评论的接口
          comment(this.comFrom)
            .then((res) => {
              console.log(res);
            })
            .catch((err) => {
              console.log(err);
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "取消评论",
          });
        });
    },
    details() {
      getbyId(this.id)
        .then((res) => {
          this.list = res.data;
          this.zan = this.list.like[0].applaud;
          this.zanFrom.UserId = this.list.article.userId;
          this.zanFrom.ArticleId = this.list.article.id;
          console.log(this.list);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    //浏览时间过了之后调用
    browse() {
      putReadNUm(this.id)
        .then((res) => {
          console.log(res);
          this.list.article.readingsNum += 1;
        })
        .catch((err) => {
          console.log(err);
        });
    },

    // 根据文章id获取评论
    getCommentByArticleId(artcileId) {
      getComment(artcileId)
        .then((res) => {
          this.getCommentFrom = res.data.comment;
          this.user = res.data.user;
          console.log(this.user);
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  mounted() {
    this.userId = getUserId();
    // this.username=getUsername();

    this.id = this.$route.query.id;
    // console.log(this.$route.query);
    console.log(this.id);
    this.details();
    this.getCommentByArticleId(this.id);

    setTimeout(() => {
      this.browse();
    }, 20000);
  },
};
</script>

<style>
.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 100wh;
}
.head {
  font-size: 50px;
  text-align: center;
}
.fool {
  text-align: center;
  margin-top: 200px;
}
.el-card__body {
  height: 700px;
  width: auto;
  overflow: auto;
  /* background:pink; */
}
.item {
  margin-top: 10px;
  margin-right: 40px;
}
</style>